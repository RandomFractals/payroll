using System;
using System.ComponentModel.DataAnnotations;

namespace payroll.Models
{
    public enum Relationship
    {
        [Display(Name = "daughter")]
        Daughter = 0,

        [Display(Name = "son")]
        Son = 1,

        [Display(Name = "spouse")]
        Spouse = 2
    }
}
