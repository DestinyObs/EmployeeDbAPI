using EmployeesAPI.Models;
using EmployeesAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        //we have to inject it here
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        //route to get all employees 
        [HttpGet("")]
        public async Task<IActionResult> GetAllEmployee()
        {
            var Employees = await _employeeRepository.GetEmployeeModelsAsync();
            return Ok(Employees);

        }

        [HttpGet("search/{searchWord}")]
        public async Task<IActionResult> SearchEmployee([FromRoute] string searchWord)
        {
            var employees = await _employeeRepository.SearchEmployeeAsync(searchWord);

            if (employees == null || employees.Count == 0)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        //route to get dictinct employees by Id
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int Id)
        {
            var Employee = await _employeeRepository.GetEmployeeByIdAsync(Id);
            if (Employee == null)
            {
                return NotFound();
            }
            return Ok(Employee);

        }

        //route to add employees 
        [HttpPost("create")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeModel employeeModel)
        {
            var id = await _employeeRepository.AddEmployeeAsync(employeeModel);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = id }, id);

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeModel employeeModel, [FromRoute] int Id)
        {
            await _employeeRepository.UpdateEmployeeAsync(Id, employeeModel);
            return Ok();

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int Id)
        {
            await _employeeRepository.DeleteEmployeeAsync(Id);
            return Ok();
        }


    }
}
