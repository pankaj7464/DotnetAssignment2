using DotnetAssignment2.Data;
using DotnetAssignment2.Dto;
using DotnetAssignment2.Entities;
namespace DotnetAssignment2.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Response> CreateDepartmentAsync(CreateUpdateDepartmentDto departmentDto)
        {
            try
            {
                var department = new Department
                {
                    Id = Guid.NewGuid(),
                    DepartmentName = departmentDto.DepartmentName
                };
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                return new Response { Message = "Department created successfully", StatusCode = 200, Data = department };
            }
            catch (Exception ex)
            {
                // Log error
                return new Response { Message = $"Error creating department: {ex.Message}", StatusCode = 500 };
            }
        }

        public async Task<Response> DeleteDepartmentAsync(Guid id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    return new Response { Message = "Department not found", StatusCode = 404 };
                }

                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();

                return new Response { Message = "Department deleted successfully", StatusCode = 200 };
            }
            catch (Exception ex)
            {
                // Log error
                return new Response { Message = $"Error deleting department: {ex.Message}", StatusCode = 500 };
            }
        }

        public async Task<Response> GetDepartmentByIdAsync(Guid id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    return new Response { Message = "Department not found", StatusCode = 404 };
                }

                return new Response { Message = "Department retrieved successfully", StatusCode = 200, Data = department };
            }
            catch (Exception ex)
            {
                // Log error
                return new Response { Message = $"Error fetching department: {ex.Message}", StatusCode = 500 };
            }
        }

        public async Task<Response> GetDepartmentsAsync()
        {
            try
            {
                var departments =  _context.Departments.ToList();
                return new Response { Message = "Departments retrieved successfully", StatusCode = 200, Data = departments };
            }
            catch (Exception ex)
            {
                // Log error
                return new Response { Message = $"Error fetching departments: {ex.Message}", StatusCode = 500 };
            }
        }

        public async Task<Response> UpdateDepartmentAsync(Guid id, CreateUpdateDepartmentDto departmentDto)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    return new Response { Message = "Department not found", StatusCode = 404 };
                }

                department.DepartmentName = departmentDto.DepartmentName;
                // Update other properties as needed

                await _context.SaveChangesAsync();

                return new Response { Message = "Department updated successfully", StatusCode = 200, Data = department };
            }
            catch (Exception ex)
            {
                // Log error
                return new Response { Message = $"Error updating department: {ex.Message}", StatusCode = 500 };
            }
        }
    }
}
