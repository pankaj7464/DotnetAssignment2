using System.ComponentModel.DataAnnotations;

namespace DotnetAssignment2.Dto
{
    public class CreateUpdateDepartmentDto
    {
        [Required]
        public required string DepartmentName { get; set; }
    }
}
