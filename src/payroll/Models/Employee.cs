using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace payroll.Models
{
	public class Employee : Person
	{
		// TODO: move these business rules to payroll service facade later
		public static decimal DefaultSalary = 2000m * 26; // $2000/check * 26 checks/year
		public static decimal DeductionPerEmployee = 1000m; // $1000/year
		public static decimal DeductionPerDependent = 500m; // $500/year

		public int EmployeeID { get; set; }

		[Display(Name = "Salary ($/year)")]
		[DisplayFormat(DataFormatString = "{0:C}")]
		[Required]
		[Range(10000, 1000000000)]
		public decimal Salary { get; set; }


		[Display(Name = "Dependents")]
		public ICollection<Dependent> Dependents { get; set; }

		[Display(Name = "Deductions ($/year)")]
		[DisplayFormat(DataFormatString = "{0:C}")]
		//[NotMapped] - not in ef7 yet
		public virtual decimal Deductions
		{
			get
			{
				// TODO: move this business logic to payroll service facade later
				_deductions = DeductionPerEmployee;
				if (FirstName.StartsWith("A"))
				{
					_deductions *= .9m; // 10% discount
				}

				if (Dependents != null)
				{
					foreach (Dependent dependent in Dependents)
					{
						decimal dependentDeduction = DeductionPerDependent;
						if (dependent.FirstName.StartsWith("A"))
						{
							dependentDeduction *= .9m; // 10% discount
						}
						_deductions += dependentDeduction;
					}
				}
				return _deductions;
			}
			set
			{
				_deductions = value;
			}
		}
		private decimal _deductions;


		public Employee()
		{

			Salary = DefaultSalary;
		}
	}
}
