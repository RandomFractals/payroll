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
    public class EmployeeController : Controller
    {
        [FromServices]
        public EmployeeDataContext EmployeeDataContext { get; set; }        


        // GET: /<controller>/
        public IActionResult Index()
        {
            var employees = EmployeeDataContext.Employees;

            return View(employees);
        }


        public async Task<ActionResult> EmployeeInfo(int id)
        {
            Employee employee = await EmployeeDataContext.Employees//.Include("Dependents")
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
                    EmployeeDataContext.Employees.Add(employee);
                    await EmployeeDataContext.SaveChangesAsync();
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
                EmployeeDataContext.Employees.Attach(employee);
                EmployeeDataContext.Entry(employee).State = EntityState.Modified;
                await EmployeeDataContext.SaveChangesAsync();
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
                EmployeeDataContext.Employees.Remove(employee);
                await EmployeeDataContext.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                return RedirectToAction("DeleteEmployee", new { id = id, retry = true });
            }

            return RedirectToAction("Index");
        }


        private Task<Employee> GetEmployeeAsync(int id)
        {
            return EmployeeDataContext.Employees.SingleOrDefaultAsync(employee => employee.EmployeeID == id);
        }
    }
}
