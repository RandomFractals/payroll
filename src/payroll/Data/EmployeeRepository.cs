using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

using payroll.Models;

namespace payroll.Data
{
	public class EmployeeRepository : IEmployeeRepository
	{

		private EmployeeDataContext _context;
		public EmployeeRepository(EmployeeDataContext context)
		{
			_context = context;
		}


		public async Task<bool> AddEmployeeAsync(Employee newEmployee)
		{
			_context.Employees.Add(newEmployee);

			return (await _context.SaveChangesAsync() > 0);
		}


		public async Task<bool> DeleteEmployeeAsync(int id)
		{
			var employee = await GetEmployeeAsync(id);

			if (employee == null) return false;

			_context.Employees.Remove(employee);

			return (await _context.SaveChangesAsync() > 0);
		}


		public async Task<Employee> GetEmployeeAsync(int id)
		{
			return await _context.Employees
					.Include(e => e.Dependents)
					.SingleOrDefaultAsync(e => e.EmployeeID == id);
		}


		public async Task<bool> UpdateEmployeeAsync(Employee employee)
		{
			_context.Employees.Update(employee);

			return (await _context.SaveChangesAsync() > 0);
		}


		public async Task<IEnumerable<Employee>> GetEmployeesAsync()
		{
			return await _context.Employees
					.Include(e => e.Dependents).ToListAsync();
		}
	}
}
