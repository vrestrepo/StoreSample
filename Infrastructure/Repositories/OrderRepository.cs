using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using AppDbContext = Infrastructure.Persistence.AppDbContext;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PredictedOrderModel>> GetPredictedOrdersAsync()
        {
            var result = await _context.Database
            .SqlQuery<PredictedOrderModel>(
                $@"SELECT 
                    c.Custid,
                    c.Companyname AS CustomerName,
                    MAX(o.Orderdate) AS LastOrderDate,
                    DATEADD(DAY, 
                        ISNULL(AVG(DATEDIFF(DAY, prev_o.Orderdate, o.Orderdate)), 30), 
                        MAX(o.Orderdate)
                    ) AS NextPredictedOrder
                FROM [Sales].[Customers] c
                JOIN [Sales].[Orders] o ON c.Custid = o.Custid
                LEFT JOIN [Sales].[Orders] prev_o 
                    ON c.Custid = prev_o.Custid 
                    AND prev_o.Orderdate < o.Orderdate
                GROUP BY c.Custid, c.Companyname"
            )
            .ToListAsync();
            return result;
        }

        public async Task<bool> ExistsCustomerAsync(int customerId)
        {
            return await _context.Customers.AnyAsync(e => e.Custid == customerId);
        }

        public async Task<IEnumerable<ClientOrderModel>> GetOrdersByClientAsync(int customerId)
        {
            return await _context.Orders
                .Where(o => o.Custid == customerId)
                .Select(o => new ClientOrderModel
                {
                    OrderId = o.Orderid,
                    RequiredDate = o.Requireddate,
                    ShippedDate = o.Shippeddate,
                    ShipName = o.Shipname,
                    ShipAddress = o.Shipaddress,
                    ShipCity = o.Shipcity
                })
                .ToListAsync();
        }

        public async Task<int> AddOrderAsync(NewOrderModel newOrder)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    Custid = newOrder.CustId,
                    Empid = newOrder.EmpId,
                    Shipperid = newOrder.ShipperId,
                    Shipname = newOrder.ShipName,
                    Shipaddress = newOrder.ShipAddress,
                    Shipcity = newOrder.ShipCity,
                    Orderdate = newOrder.OrderDate,
                    Requireddate = (DateTime)(newOrder.RequiredDate == null ? DateTime.Now : newOrder.RequiredDate),
                    Shippeddate = newOrder.ShippedDate,
                    Freight = newOrder.Freight,
                    Shipcountry = newOrder.ShipCountry
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                var orderDetails = newOrder.OrderDetails.Select(detail => new OrderDetail
                {
                    Orderid = order.Orderid,
                    Productid = detail.ProductId,
                    Unitprice = detail.UnitPrice,
                    Qty = (short)detail.Qty,
                    Discount = (decimal)detail.Discount
                }).ToList();
                await _context.OrderDetails.AddRangeAsync(orderDetails);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return order.Orderid;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en repositorio Order", ex.ToString());
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}