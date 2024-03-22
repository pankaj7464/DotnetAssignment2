using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetAssignment2.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Range(21, 100)]
        public int Age { get; set; }

        [Required]
        public decimal Salary { get; set; }
        [Required]

        [ForeignKey(nameof(DepartmentId))]
        public Guid DepartmentId { get; set; }
        public  Department Department { get; set; }
    }
}
