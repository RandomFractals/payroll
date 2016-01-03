﻿using System;
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
            Employee employee = await //Task.WhenAll(
                EmployeeDataContext.Employees
                .SingleOrDefaultAsync(e => e.EmployeeID == id);
                //);

            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.EmployeeID = employee.EmployeeID;
            ViewBag.EmployeeName = employee.FirstName + ' ' + employee.LastName;

            return View();
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
                //.SingleOrDefaultAsync(d => d.DependentID == id);
                .Include(e => e.Employee)
                .SingleOrDefaultAsync(d => d.DependentID == id);

        }
    }
}
