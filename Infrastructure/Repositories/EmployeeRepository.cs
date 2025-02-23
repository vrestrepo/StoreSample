using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using AppDbContext = Infrastructure.Persistence.AppDbContext;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployeesAsync()
        {
            return await _context.Employees
                .Select(e => new EmployeeModel
                {
                    EmpId = e.Empid,
                    FullName = e.Firstname + " " + e.Lastname
                })
                .ToListAsync();
        }
    }
}