using System.ComponentModel.DataAnnotations;

namespace payroll.Models
{
    public class Person
    {
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set;  }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set;  }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
    }
}
