using System.ComponentModel.DataAnnotations;

namespace payroll.Models
{
    public class Dependent: Person
    {
        public int DependentID { get; set; }

        [Display(Name = "Relationship")]
        public Relationship Relationship { get; set; }

        // navigation property
        public Employee Employee { get; set; }
    }
}
