using EmployeeApi.Models;

namespace EmployeeApi.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<int> CreateEmployeeAsync(Employee employee);
        Task<int> UpdateEmployeeAsync(Employee employee);
        Task<int> DeleteEmployeeAsync(int id);

    }
}
