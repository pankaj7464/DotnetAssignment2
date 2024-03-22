
using DotnetAssignment2.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetAssignment2.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext>  option):base(option)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }




       
    }
}
