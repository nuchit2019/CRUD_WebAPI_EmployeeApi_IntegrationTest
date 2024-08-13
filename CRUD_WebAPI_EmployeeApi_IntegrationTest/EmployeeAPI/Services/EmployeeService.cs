using AutoMapper;
using EmployeeApi.Models;
using EmployeeApi.Repository;

namespace EmployeeApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<int> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            return await _employeeRepository.CreateEmployeeAsync(employee);
        }

        public async Task<int> UpdateEmployeeAsync(int id, UpdateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            employee.Id = id;
            return await _employeeRepository.UpdateEmployeeAsync(employee);
        }

        public async Task<int> DeleteEmployeeAsync(int id)
        {
            return await _employeeRepository.DeleteEmployeeAsync(id);
        }
    }
}
