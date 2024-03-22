using Microsoft.AspNetCore.Mvc;
using DotnetAssignment2.Repositories;
using DotnetAssignment2.Dto;

namespace DotnetAssignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentRepository departmentRepository, ILogger<DepartmentController> logger)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> GetDepartmentsAsync()
        {
            try
            {
                var departments = await _departmentRepository.GetDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching departments");
                return StatusCode(500, new Response { Message = "Internal server error", StatusCode = 500 });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetDepartmentByIdAsync(Guid id)
        {
            try
            {
                var department = await _departmentRepository.GetDepartmentByIdAsync(id);
                if (department == null)
                {
                    return NotFound(new Response { Message = "Department not found", StatusCode = 404 });
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching department by ID");
                return StatusCode(500, new Response { Message = "Internal server error", StatusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response>> AddDepartmentAsync(CreateUpdateDepartmentDto department)
        {
            try
            {
                var addedDepartment = await _departmentRepository.CreateDepartmentAsync(department);
                return Ok(  addedDepartment );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding department");
                return StatusCode(500, new Response { Message = "Internal server error", StatusCode = 500 });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateDepartment(Guid id, CreateUpdateDepartmentDto department)
        {
            try
            {
              

                var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(id);
                if (existingDepartment == null)
                {
                    return NotFound(new Response { Message = "Department not found", StatusCode = 404 });
                }

               var updatedDepartment =  await _departmentRepository.UpdateDepartmentAsync(id,department);
                return Ok(updatedDepartment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating department");
                return StatusCode(500, new Response { Message = "Internal server error", StatusCode = 500 });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteDepartmentAsync(Guid id)
        {
            try
            {
                var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(id);
                if (existingDepartment == null)
                {
                    return NotFound(new Response { Message = "Department not found", StatusCode = 404 });
                }

               var res =  await _departmentRepository.DeleteDepartmentAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting department");
                return StatusCode(500, new Response { Message = "Internal server error", StatusCode = 500 });
            }
        }
    }
}
