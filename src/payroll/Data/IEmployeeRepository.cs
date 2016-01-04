using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using payroll.Models;

namespace payroll.Data
{
    interface IEmployeeRepository
    {
        Task<bool> AddEmployeeAsync(Employee newEmployee);

        Task<bool> DeleteEmployeeAsync(int id);

        Task<Employee> GetEmployeeAsync(int id);

        Task<bool> UpdateEmployeeAsync(Employee employee);

        Task<IEnumerable<Employee>> GetAllEmployees();
    }
}
