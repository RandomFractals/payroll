using System.ComponentModel.DataAnnotations;

namespace payroll.Models
{
    public class Employee : Person
    {
        public int EmployeeID { get; set; }

        [Display(Name = "Salary")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Salary { get; set;  }
    }
}
