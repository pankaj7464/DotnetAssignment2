using System.ComponentModel.DataAnnotations;

namespace DotnetAssignment2.Entities
{
    public class Department
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string DepartmentName { get; set; }


    }
}
