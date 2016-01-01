using Microsoft.Data.Entity;

namespace payroll.Models
{
    public class EmployeeContext: DbContext
    {
        public DbSet<Employee> Employees { get; set;  }
        public DbSet<Dependent> Dependents { get; set;  }
    }
}
