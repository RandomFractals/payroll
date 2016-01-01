using Microsoft.Data.Entity;

namespace payroll.Models
{
    public class EmployeeContext: DbContext
    {
        public DbSet<Employee> Employees { get; set;  }
    }
}
