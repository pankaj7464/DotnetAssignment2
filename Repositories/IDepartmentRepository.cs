using DotnetAssignment2.Dto;
using DotnetAssignment2.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAssignment2.Repositories
{
    public interface IDepartmentRepository
    {


        public Task<Response> CreateDepartmentAsync(CreateUpdateDepartmentDto department);
        public Task<Response> GetDepartmentsAsync();
        public Task<Response> GetDepartmentByIdAsync(Guid id);
        public  Task<Response> UpdateDepartmentAsync(Guid id, CreateUpdateDepartmentDto department);
        public  Task<Response> DeleteDepartmentAsync(Guid id);
    }
}
