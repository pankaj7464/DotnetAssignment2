using DotnetAssignment2.Dto;

namespace DotnetAssignment2.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<Response> CreateEmployeeAsync(CreateUpdateEmployeeDto emp);
        public Task<Response> GetEmployeesAsync();
        public Task<Response> GetEmployeeByIdAsync(Guid id);
        public Task<Response> UpdateEmployeeAsync(Guid id, CreateUpdateEmployeeDto emp);
        public Task<Response> DeleteEmployeeAsync(Guid id);
    }
}
