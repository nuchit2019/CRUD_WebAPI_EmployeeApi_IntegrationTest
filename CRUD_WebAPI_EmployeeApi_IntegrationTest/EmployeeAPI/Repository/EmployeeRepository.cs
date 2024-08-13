using EmployeeApi.Data;
using EmployeeApi.Models;

namespace EmployeeApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public EmployeeRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            const string sql = "SELECT * FROM Employees";
            return await _sqlDataAccess.QueryAsync<Employee>(sql);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Employees WHERE Id = @Id";
            return await _sqlDataAccess.QuerySingleAsync<Employee>(sql, new { Id = id });
        }

        public async Task<int> CreateEmployeeAsync(Employee employee)
        {
            const string sql = "INSERT INTO Employees (Name, Position, Salary) VALUES (@Name, @Position, @Salary)";
            return await _sqlDataAccess.ExecuteAsync(sql, employee);
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee)
        {
            const string sql = "UPDATE Employees SET Name = @Name, Position = @Position, Salary = @Salary WHERE Id = @Id";
            return await _sqlDataAccess.ExecuteAsync(sql, employee);
        }

        public async Task<int> DeleteEmployeeAsync(int id)
        {
            const string sql = "DELETE FROM Employees WHERE Id = @Id";
            return await _sqlDataAccess.ExecuteAsync(sql, new { Id = id });
        }

    }
}
