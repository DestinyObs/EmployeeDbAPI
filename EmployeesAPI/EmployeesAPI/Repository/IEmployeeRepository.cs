using EmployeesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAPI.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetEmployeeModelsAsync();
        //calling each of the method from the repository
        Task<EmployeeModel> GetEmployeeByIdAsync(int EmpId);
        Task<int> AddEmployeeAsync(EmployeeModel employeeModel);
        Task DeleteEmployeeAsync(int EmpId);
        Task UpdateEmployeeAsync(int EmpId, EmployeeModel employeeModel);
        Task<List<EmployeeModel>> SearchEmployeeAsync(string searchWord);
    }
}
