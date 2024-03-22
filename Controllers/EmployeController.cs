using DotnetAssignment2.Dto;
using DotnetAssignment2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAssignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;
     

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> GetemployeesAsync()
        {
            try
            {
                var response = await _employeeRepository.GetEmployeesAsync();
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching employees");
                return StatusCode(500, new Response { Message = "Internal server error", StatusCode = 500 });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetemployeeByIdAsync(Guid id)
        {
            try
            {
                var response = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (response == null)
                {
                    return NotFound(new Dto.Response { Message = "employee not found", StatusCode = 404 });
                }
                 return StatusCode(response.StatusCode, response); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching employee by ID");
                return StatusCode(500, new Response { Message = "Internal server error", StatusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response>> AddemployeeAsync(CreateUpdateEmployeeDto employee)
        {
            try
            {
                var response = await _employeeRepository.CreateEmployeeAsync(employee);
                 return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding employee");
                return StatusCode(500, new Response { Message = "Internal server error", StatusCode = 500 });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateEmployeeAsync(Guid id, CreateUpdateEmployeeDto employee)
        {
            try
            {


                var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound(new Response { Message = "employee not found", StatusCode = 404 });
                }

                var response = await _employeeRepository.UpdateEmployeeAsync(id, employee);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating employee");
                return StatusCode(500, new Response { Message = "Internal server error", StatusCode = 500 });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteemployeeAsync(Guid id)
        {
            try
            {
                var existingemployee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (existingemployee == null)
                {
                    return NotFound(new Response { Message = "employee not found", StatusCode = 404 });
                }

                var response = await _employeeRepository.DeleteEmployeeAsync(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting employee");
                return StatusCode(500, new Response { Message = "Internal server error", StatusCode = 500 });
            }
        }


    }
}
