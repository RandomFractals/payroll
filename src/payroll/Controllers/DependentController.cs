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
    public class DependentController : Controller
    {

        [FromServices]
        public EmployeeDataContext EmployeeDataContext { get; set; }

        public async Task<ActionResult> EmployeeInfo(int id)
        {
            Employee employee = await EmployeeDataContext.Employees
                .Include(e => e.Dependents)
                .SingleOrDefaultAsync(e => e.EmployeeID == id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }


        public async Task<ActionResult> AddDependent(int id)
        {
            Employee employee = await EmployeeDataContext.Employees
                .SingleOrDefaultAsync(e => e.EmployeeID == id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.EmployeeID = employee.EmployeeID;
            ViewBag.EmployeeName = employee.FirstName + ' ' + employee.LastName;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveDependent(
            [Bind("FirstName", "LastName", "Relationship", "EmployeeID")] Dependent dependent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeDataContext.Dependents.Add(dependent);
                    await EmployeeDataContext.SaveChangesAsync();

                    return RedirectToAction("Dependents", "Employee",
                        new { id = dependent.EmployeeID });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Could not add new dependent.");
            }

            return View(dependent);
        }


        public async Task<ActionResult> EditDependent(int id)
        {
            Dependent dependent = await GetDependentAsync(id);
            if (dependent == null)
            {
                return HttpNotFound();
            }
            return View(dependent);
        }


        public async Task<ActionResult> UpdateDependent(int id,
            [Bind("FirstName", "LastName", "Relationship")] Dependent dependent)
        {
            try
            {
                dependent.DependentID = id;
                EmployeeDataContext.Dependents.Attach(dependent);
                EmployeeDataContext.Entry(dependent).State = EntityState.Modified;
                await EmployeeDataContext.SaveChangesAsync();
                return RedirectToAction("Dependents", "Employee", 
                    new { id = dependent.Employee.EmployeeID });
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Failed to save dependent info updates.");
            }
            return View(dependent);
        }


        [HttpGet]
        [ActionName("DeleteDependent")]
        public async Task<ActionResult> ConfirmDelete(int id, bool? retry)
        {
            Dependent dependent = await GetDependentAsync(id);
            if (dependent == null)
            {
                return HttpNotFound();
            }

            ViewBag.Retry = retry ?? false;

            return View(dependent);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDependent(int id)
        {
            int employeeId;
            try
            {
                Dependent dependent = await GetDependentAsync(id);
                employeeId = dependent.Employee.EmployeeID;
                EmployeeDataContext.Dependents.Remove(dependent);
                await EmployeeDataContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("DeleteDependent", new { id = id, retry = true });
            }

            return RedirectToAction("Dependents", "Employee", new { id = employeeId });
        }


        private Task<Dependent> GetDependentAsync(int id)
        {
            return EmployeeDataContext.Dependents
                .Include(e => e.Employee)
                .SingleOrDefaultAsync(d => d.DependentID == id);

        }
    }
}
