using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using payroll.Models;

namespace payroll.Controllers
{
    public class EmployeeController : Controller
    {
        [FromServices]
        public EmployeeContext EmployeeContext { get; set; }        

        // GET: /<controller>/
        public IActionResult Index()
        {
            var employees = EmployeeContext.Employees;

            return View(employees);
        }
    }
}
