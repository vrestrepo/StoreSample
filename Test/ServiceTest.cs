using Application.Services;
using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Test
{
    public class ServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepo;
        private readonly EmployeeService _employeeService;

        private readonly Mock<IOrderRepository> _mockOrderRepo;
        private readonly OrderService _orderService;

        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly ProductService _productService;

        private readonly Mock<IShipperRepository> _mockShipperRepo;
        private readonly ShipperService _shipperService;

        public ServiceTests()
        {
            _mockEmployeeRepo = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_mockEmployeeRepo.Object);

            _mockOrderRepo = new Mock<IOrderRepository>();
            _orderService = new OrderService(_mockOrderRepo.Object);

            _mockProductRepo = new Mock<IProductRepository>();
            _productService = new ProductService(_mockProductRepo.Object);

            _mockShipperRepo = new Mock<IShipperRepository>();
            _shipperService = new ShipperService(_mockShipperRepo.Object);
        }

        [Fact]
        public async Task GetEmployeesAsync_ReturnsEmployees()
        {
            var employees = new List<EmployeeModel> { new() { EmpId = 1, FullName = "Juan" } };
            _mockEmployeeRepo.Setup(repo => repo.GetEmployeesAsync()).ReturnsAsync(employees);

            var result = await _employeeService.GetEmployeesAsync();

            Assert.True(result.Success);
            Assert.Equal("Lista de todos los empleados", result.Message);
            Assert.Equal(employees, result.Data);
        }

        [Fact]
        public async Task GetProductsAsync_ReturnsProducts()
        {
            var products = new List<ProductModel> { new() { ProductId = 1, ProductName = "Producto A" } };
            _mockProductRepo.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(products);

            var result = await _productService.GetProductsAsync();

            Assert.True(result.Success);
            Assert.Equal("Lista de productos", result.Message);
            Assert.Equal(products, result.Data);
        }

        [Fact]
        public async Task GetShippersAsync_ReturnsShippers()
        {
            var shippers = new List<ShipperModel> { new() { ShipperId = 1, CompanyName = "Transportes XYZ" } };
            _mockShipperRepo.Setup(repo => repo.GetShippersAsync()).ReturnsAsync(shippers);

            var result = await _shipperService.GetShippersAsync();

            Assert.True(result.Success);
            Assert.Equal("Lista de transportistas", result.Message);
            Assert.Equal(shippers, result.Data);
        }

        [Fact]
        public async Task AddOrderAsync_ReturnsSuccess()
        {
            var newOrder = new NewOrderModel { EmpId = 1, ShipperId = 2 };
            _mockOrderRepo.Setup(repo => repo.AddOrderAsync(newOrder)).ReturnsAsync(1);

            var result = await _orderService.AddOrderAsync(newOrder);

            Assert.True(result.Success);
            Assert.Equal("Orden creada exitosamente", result.Message);
            Assert.Equal(1, result.Data);
        }

        [Fact]
        public async Task AddOrderAsync_ReturnsSuccessFull()
        {
            var newOrder = new NewOrderModel
            {
                
                EmpId = 3,
                ShipperId = 2,
                ShipName = "Colombia 1",
                ShipAddress = "Calle 123",
                ShipCity = "Manizales",
                OrderDate = DateTime.Parse("2025-02-22T14:30:00"),
                RequiredDate = DateTime.Parse("2025-03-01T14:30:00"),
                ShippedDate = DateTime.Parse("2025-02-25T10:00:00"),
                Freight = 15.50m,
                ShipCountry = "Colombia",
                OrderDetails = new List<OrderDetailModel>
            {
                new OrderDetailModel { ProductId = 5, UnitPrice = 20.99m, Qty = 2 }
            }
            };

            _mockOrderRepo.Setup(repo => repo.AddOrderAsync(newOrder)).ReturnsAsync(1);

            var result = await _orderService.AddOrderAsync(newOrder);

            Assert.True(result.Success);
            Assert.Equal("Orden creada exitosamente", result.Message);
            Assert.Equal(1, result.Data);
        }

        [Fact]
        public async Task AddOrderAsync_ReturnsError_WhenRepositoryFails()
        {
            var newOrder = new NewOrderModel
            {
                EmpId = 3,
                ShipperId = 2,
                ShipName = "Colombia 1",
                ShipAddress = "Calle 123",
                ShipCity = "Manizales",
                OrderDate = DateTime.Parse("2025-02-22T14:30:00"),
                RequiredDate = DateTime.Parse("2025-03-01T14:30:00"),
                ShippedDate = DateTime.Parse("2025-02-25T10:00:00"),
                Freight = 15.50m,
                ShipCountry = "Colombia",
                OrderDetails = new List<OrderDetailModel>
            {
                new OrderDetailModel { ProductId = 5, UnitPrice = 20.99m, Qty = 2 }
            }
            };

            _mockOrderRepo.Setup(repo => repo.AddOrderAsync(newOrder)).ThrowsAsync(new Exception("DB error"));

            var result = await _orderService.AddOrderAsync(newOrder);

            Assert.False(result.Success);
            Assert.Equal("Error al crear la orden", result.Message);
            Assert.Equal(0, result.Data);
        }

        [Fact]
        public async Task AddOrderAsync_ReturnsError()
        {
            var newOrder = new NewOrderModel { EmpId = 1, ShipperId = 2 };
            _mockOrderRepo.Setup(repo => repo.AddOrderAsync(newOrder)).ThrowsAsync(new System.Exception());

            var result = await _orderService.AddOrderAsync(newOrder);

            Assert.False(result.Success);
            Assert.Equal("Error al crear la orden", result.Message);
            Assert.Equal(0, result.Data);
        }
    }
}
