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
        public Task<bool> AddEmployeeAsync(Employee newEmployee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAllEmployees()
        {
            throw new NotImplementedException();
        }
    }
}
