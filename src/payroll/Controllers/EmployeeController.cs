using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

using payroll.Models;
using payroll.Data;

namespace payroll.Controllers
{
	public class EmployeeController : Controller
	{
		
		[FromServices]
		public EmployeeRepository EmployeeRepository { get; set; }
		

		// GET: /<controller>/
		public async Task<ActionResult> Index(string sortOrder, string searchString)
		{
			// get employees
			// TODO: add pagination with Skip(PageIndex*ItemsPerPage) and Take(ItermsPerPage)
			var employees = await EmployeeRepository.GetEmployeesAsync();

			// init list view sort order
			ViewBag.NameSortOrder = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
			ViewBag.SalarySortOrder = (sortOrder == "Salary" ? "SalaryDesc" : "Salary");
			ViewBag.DeductionsSortOrder = (sortOrder == "Deductions" ? "DeductionsDesc" : "Deductions");

			if (!String.IsNullOrEmpty(searchString))
			{
				// filter by last name
				employees = employees.Where(e => e.LastName.Contains(searchString)).ToList();
			}

			// sort employees for list view
			switch (sortOrder)
			{
				case "Salary":
					employees = employees.OrderBy(e => e.Salary).ToList();
					break;
				case "SalaryDesc":
					employees = employees.OrderByDescending(e => e.Salary).ToList();
					break;
				case "Deductions":
					employees = employees.OrderBy(e => e.Deductions).ToList();
					break;
				case "DeductionsDesc":
					employees = employees.OrderByDescending(e => e.Deductions).ToList();
					break;
				case "NameDesc":
					employees = employees.OrderByDescending(e => e.LastName).ToList();
					break;
				default:
					employees = employees.OrderBy(e => e.LastName).ToList();
					break;
			}


			return View(employees);
		}


		public async Task<ActionResult> Dependents(int id)
		{
			Employee employee = await EmployeeRepository.GetEmployeeAsync(id);
			if (employee == null)
			{
				return View("NotFoundError");
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
				[Bind("FirstName", "LastName", "Salary", "Deductions")] Employee employee)
		{
			try
			{
				if (ModelState.IsValid)
				{
					await EmployeeRepository.AddEmployeeAsync(employee);
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
			Employee employee = await EmployeeRepository.GetEmployeeAsync(id);
			if (employee == null)
			{
				return View("NotFoundError");
			}

			return View(employee);
		}


		public async Task<ActionResult> UpdateEmployee(int id,
				[Bind("FirstName", "LastName", "Salary")] Employee employee)
		{
			try
			{
				employee.EmployeeID = id;
				await EmployeeRepository.UpdateEmployeeAsync(employee);
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
			Employee employee = await EmployeeRepository.GetEmployeeAsync(id);
			if (employee == null)
			{
				return View("NotFoundError");
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
				await EmployeeRepository.DeleteEmployeeAsync(id);
			}
			catch (DbUpdateException)
			{
				return RedirectToAction("DeleteEmployee", new { id = id, retry = true });
			}

			return RedirectToAction("Index");
		}
	}
}
