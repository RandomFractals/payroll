using System;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Entity.Storage;

namespace payroll.Models
{
    public static class SampleData
    {
		public static void Initialize(IServiceProvider serviceProvider)
		{

            var context = serviceProvider.GetService<EmployeeDataContext>();
			if ( serviceProvider.GetService<IRelationalDatabaseCreator>().Exists() )
			{
				var janeDoe = context.Employees.Add(
					new Employee { FirstName = "Jane", LastName = "Doe", Salary = 52000}).Entity;

                context.Dependents.AddRange(
                    new Dependent()
                    {
                        FirstName = "Jon",
                        LastName = "Don",
                        Employee = janeDoe,
                        Relationship = Relationship.Spouse
                    },
                    new Dependent()
                    {
                        FirstName = "Sally",
                        MiddleName = "Madison",
                        LastName = "Don-Doe",
                        Relationship = Relationship.Daughter
                    }
                );

                context.SaveChanges();
					
			}
		}
    }
}
