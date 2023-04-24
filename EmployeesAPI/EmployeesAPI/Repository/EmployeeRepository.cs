using EmployeesAPI.Data;
using EmployeesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }
        
        //getting all employees avaiable 
        public async Task<List<EmployeeModel>> GetEmployeeModelsAsync()
        {
            var  records = _context.EmployeesDB.Select(x => new EmployeeModel() 
            { 
                Id = x.Id,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Gender = x.Gender,
                Salary = x.Salary
              
            } ).ToListAsync();

           return await records;

        }

        //making any random search that brings out an output
        public async Task<List<EmployeeModel>> SearchEmployeeAsync(string searchWord)
        {
            var search = await _context.EmployeesDB
                .Where(x => x.LastName.Contains(searchWord) || x.FirstName.Contains(searchWord) || x.Gender.Contains(searchWord))
                .Select(x => new EmployeeModel()
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Gender = x.Gender,
                    Salary = x.Salary
                })
                .ToListAsync();

            return search;
        }

        //getting distinct employees avaiable using their Id 

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int EmpId)
        {
            var records =  _context.EmployeesDB.Where(x => x.Id == EmpId).Select(x => new EmployeeModel()
            {
                Id = x.Id,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Gender = x.Gender,
                Salary = x.Salary

            }).FirstOrDefault();

            return records;

        }
        //Adding employees 
        public async Task<int> AddEmployeeAsync(EmployeeModel employeeModel)
        {
            var employee = new Employees()
            {
                LastName = employeeModel.LastName,
                FirstName = employeeModel.FirstName,
                Gender = employeeModel.Gender,
                Salary = employeeModel.Salary
            };

            _context.EmployeesDB.Add(employee);
           await _context.SaveChangesAsync();

            return employee.Id;
        }

        //Deleting Employee by Id
        public async Task DeleteEmployeeAsync(int EmpId)
        {
            var employee = new Employees() { Id = EmpId };
            _context.EmployeesDB.Remove(employee);
            await _context.SaveChangesAsync();

        }

        //Updating the value of an employee
        public async Task UpdateEmployeeAsync(int EmpId, EmployeeModel employeeModel)
        {
            var employee = await _context.EmployeesDB.FindAsync(EmpId);
            if (employee != null)
            {
                employee.Id = employeeModel.Id;
                employee.LastName = employeeModel.LastName;
                employee.FirstName = employeeModel.FirstName;
                employee.Gender = employeeModel.Gender;
                employee.Salary = employeeModel.Salary;

                _context.SaveChangesAsync();
            }

        }


    }

}
