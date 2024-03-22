using AutoMapper;
using DotnetAssignment2.Data;
using DotnetAssignment2.Dto;
using DotnetAssignment2.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetAssignment2.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeRepository> _logger; 

        public EmployeeRepository(DataContext context, IMapper mapper, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        //Create new Employee 
        public async Task<Response> CreateEmployeeAsync(CreateUpdateEmployeeDto employee)
        {
            try
            {
                var employeeDto = _mapper.Map<Employee>(employee);
                await _context.Employees.AddAsync(employeeDto);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Employee created successfully");

                return new Response { Message = "Employee created successfully", StatusCode = 200, Data = employeeDto };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating Employee: {ex.Message}");
                return new Response { Message = $"Error creating Employee: {ex.Message}", StatusCode = 500 };
            }
        }

        //Delete employee from system 
        public async Task<Response> DeleteEmployeeAsync(Guid id)
        {
            try
            {
                var emp = await _context.Employees.FindAsync(id);
                if (emp == null)
                {
                    return new Response { Message = "Employee not found", StatusCode = 404 };
                }
                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Employee deleted successfully");

                return new Response { Message = "Employee deleted successfully", StatusCode = 200 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting employee: {ex.Message}");

                return new Response { Message = $"Error deleting employee: {ex.Message}", StatusCode = 500 };
            }
        }

        //Get employee by their Id
        public async Task<Response> GetEmployeeByIdAsync(Guid id)
        {
            try
            {
                var employee = await _context.Employees
                    .Include(emp => emp.Department)
                     .Select(emp => new
                     {
                         emp.Id,
                         emp.Name,
                         emp.Salary,
                         emp.Department.DepartmentName,
                         emp.DepartmentId,
                         emp.Age
                     })
                    .FirstOrDefaultAsync(emp => emp.Id == id);

                if (employee == null)
                {
                    return new Response { Message = "Employee not found", StatusCode = 404 };
                }

                _logger.LogInformation("Employee retrieved successfully");

                return new Response { Message = "Employee retrieved successfully", StatusCode = 200, Data = employee };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching employee: {ex.Message}");

                return new Response { Message = $"Error fetching employee: {ex.Message}", StatusCode = 500 };
            }
        }

        //Get all employee
        public async Task<Response> GetEmployeesAsync()
        {
            try
            {
                var employee = await _context.Employees.Include(depart => depart.Department)
                    .Select(emp => new
                    {
                        emp.Id,
                        emp.Name,
                        emp.Salary,
                        emp.Department.DepartmentName,
                        emp.DepartmentId,
                        emp.Age
                    })
                    .ToListAsync();

                _logger.LogInformation("Employees retrieved successfully");

                return new Response { Message = "Employees retrieved successfully", StatusCode = 200, Data = employee };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching employee: {ex.Message}");

                return new Response { Message = $"Error fetching employee: {ex.Message}", StatusCode = 500 };
            }
        }

        //Update employee 
        public async Task<Response> UpdateEmployeeAsync(Guid id, CreateUpdateEmployeeDto employeeDto)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    return new Response { Message = "Employee not found", StatusCode = 404 };
                }

                // Update properties of the existing employee entity
                _mapper.Map(employeeDto, employee);

                // Save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("Employee updated successfully");

                return new Response { Message = "Employee updated successfully", StatusCode = 200, Data = employee };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating employee: {ex.Message}");

                return new Response { Message = $"Error updating employee: {ex.Message}", StatusCode = 500 };
            }
        }
    }
}
