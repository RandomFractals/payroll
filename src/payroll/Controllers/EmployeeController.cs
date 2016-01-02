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
        public EmployeeDataContext EmployeeContext { get; set; }        


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


        public ActionResult AddEmployee()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveEmployee(
            [Bind("FirstName", "LastName", "Salary")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeContext.Employees.Add(employee);
                    await EmployeeContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Could not create new employee.");
            }

            return View(employee);
        }


        public async Task<ActionResult> EditEmployee(int id)
        {
            Employee employee = await GetEmployeeAsync(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }


        public async Task<ActionResult> UpdateEmployee(int id, 
            [Bind("FirstName", "LastName", "Salary")] Employee employee)
        {
            try
            {
                employee.EmployeeID = id;
                EmployeeContext.Employees.Attach(employee);
                EmployeeContext.Entry(employee).State = EntityState.Modified;
                await EmployeeContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Failed to save employee info updates.");
            }
            return View(employee);
        }


        [HttpGet]
        [ActionName("DeleteEmployee")]
        public async Task<ActionResult> ConfirmDelete(int id, bool? retry)
        {
            Employee employee = await GetEmployeeAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.Retry = retry ?? false;

            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                Employee employee = await GetEmployeeAsync(id);
                EmployeeContext.Employees.Remove(employee);
                await EmployeeContext.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                return RedirectToAction("DeleteEmployee", new { id = id, retry = true });
            }

            return RedirectToAction("Index");
        }


        private Task<Employee> GetEmployeeAsync(int id)
        {
            return EmployeeContext.Employees.SingleOrDefaultAsync(employee => employee.EmployeeID == id);
        }
    }
}
