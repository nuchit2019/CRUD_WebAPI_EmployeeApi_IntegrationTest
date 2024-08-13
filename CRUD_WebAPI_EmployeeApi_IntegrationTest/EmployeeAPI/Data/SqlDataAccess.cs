using Microsoft.Data.SqlClient;
using Dapper;

namespace EmployeeApi.Data
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
            }
        }

        public async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
