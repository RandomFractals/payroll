using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace payroll.Models
{
    public class Employee : Person
    {
        public int EmployeeID { get; set; }

        [Display(Name = "Salary")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required]
        [Range(10000, 1000000000)]
        public decimal Salary { get; set;  }


        [Display(Name = "Dependents")]
        public ICollection<Dependent> Dependents { get; set; }
    }
}
