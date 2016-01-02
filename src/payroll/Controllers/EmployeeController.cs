using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;

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

        public async Task<ActionResult> EmployeeInfo(int id)
        {
            Employee employee = await EmployeeContext.Employees
                .SingleOrDefaultAsync(e => e.EmployeeID == id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        public ActionResult EditEmployee()
        {
            return View();
        }
    }
}
