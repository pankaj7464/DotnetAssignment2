using AutoMapper;
using DotnetAssignment2.Dto;
using DotnetAssignment2.Entities;

namespace DotnetAssignment2.ObjectMapping
{
    public class ObjectMapper:Profile
    {
        public ObjectMapper()
        {
        CreateMap<CreateUpdateEmployeeDto, Employee>();
        CreateMap<Employee, CreateUpdateEmployeeDto>();

            
        }
    }
}
