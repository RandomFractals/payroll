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

        /*
        public IActionResult AddDependent(int employeeId)
        {
            Employee employee = EmployeeDataContext.Employees
                .SingleOrDefault(e => e.EmployeeID == employeeId);

            ViewBag.Employee = employee;

            return View();
        }*/

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


        private Task<Dependent> GetDependentAsync(int id)
        {
            return EmployeeDataContext.Dependents
                .SingleOrDefaultAsync(d => d.DependentID == id);
        }
    }
}
