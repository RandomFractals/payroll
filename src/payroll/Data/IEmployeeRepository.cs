using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using payroll.Models;

namespace payroll.Data
{
    interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();

        Task<bool> AddEmployeeAsync(Employee newEmployee);

        Task<bool> UpdateEmployeeAsync(Employee employee);

        Task<Employee> GetEmployeeByIdAsync(int id);

        Task<bool> DeleteEmployeeAsync(int id);
    }
}
