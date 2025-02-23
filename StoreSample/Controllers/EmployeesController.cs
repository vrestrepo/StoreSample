using Application.Common;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoreSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<EmployeeModel>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<EmployeeModel>>), 400)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<EmployeeModel>>>> GetEmployees()
        {
            var response = await _employeeService.GetEmployeesAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
