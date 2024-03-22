using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotnetAssignment2.Dto
{
    public class CreateUpdateEmployeeDto
    {
        public string Name { get; set; }

        [Range(21, 100)]
        public int Age { get; set; }

        [Required]
        public decimal Salary { get; set; }
        [Required]

      
        public Guid DepartmentId { get; set; }
    }
}
