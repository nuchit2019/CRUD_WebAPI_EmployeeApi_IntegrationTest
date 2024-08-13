using EmployeeApi.Models;

namespace EmployeeApi.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<int> CreateEmployeeAsync(CreateEmployeeDto employeeDto);
        Task<int> UpdateEmployeeAsync(int id, UpdateEmployeeDto employeeDto);
        Task<int> DeleteEmployeeAsync(int id);

    }
}
