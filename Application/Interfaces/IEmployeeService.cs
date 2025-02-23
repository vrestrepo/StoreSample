using Application.Common;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<IEnumerable<EmployeeModel>>> GetEmployeesAsync();
    }
}