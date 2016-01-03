using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace payroll.Models
{
    public class Employee : Person
    {
        // TODO: move these business rules to payroll service facade later
        public static decimal DefaultSalary = 2000 * 26; // $2000/check * 26 checks/year
        public static decimal DeductionPerEmployee = 1000; // $1000/year
        public static decimal DeductionPerDependent = 500; // $500/year

        public int EmployeeID { get; set; }

        [Display(Name = "Salary ($)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required]
        [Range(10000, 1000000000)]
        public decimal Salary { get; set;  }


        [Display(Name = "Dependents")]
        public ICollection<Dependent> Dependents { get; set; }

        [Display(Name = "Deductions")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        //[NotMapped] - not in ef7 yet
        public virtual decimal Deductions
        {
            get
            {
                // TODO: move this business logic to payroll service facade later
                _deduction = DeductionPerEmployee;
                if ( FirstName.StartsWith("A") )
                {
                    _deduction *= .01m; // 10% discount
                }

                return _deduction;
            }
            set {
                _deduction = value;
            }
        }
        private decimal _deduction;


        public Employee()
        {

            Salary = DefaultSalary;
        }
    }
}
