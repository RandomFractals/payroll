using Microsoft.Data.Entity;

namespace payroll.Models
{
    public class EmployeeDataContext: DbContext
    {
        public DbSet<Employee> Employees { get; set;  }
        public DbSet<Dependent> Dependents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Ignore(e => e.Deductions);
        }
    }
}
