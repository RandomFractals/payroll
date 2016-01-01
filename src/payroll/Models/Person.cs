using System.ComponentModel.DataAnnotations;

namespace payroll.Models
{
    public class Person
    {
        public int PersonID { get; set;  }

        [Display(Name = "First Name")]
        public string FirstName { get; set;  }

        [Display(Name = "Last Name")]
        public string LastName { get; set;  }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
    }
}
