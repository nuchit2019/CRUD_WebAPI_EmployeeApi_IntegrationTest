using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EmployeeApi.Controllers;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;

using Xunit; 

namespace EmployeeApi2.Test
{
    public class EmployeeApiIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EmployeeApiIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact(DisplayName = "GetAllEmployees: ดึง Employee ทั้งหมด ต้องได้ HttpStatusCode = OK")]
        public async Task GetAllEmployees_ReturnsOkResponse_WithEmployeeList()
        {
            // Arrange
            var url = "/api/Employees";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var employees = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();
            Assert.NotNull(employees);
            Assert.NotEmpty(employees);
        }

        [Fact(DisplayName = "GetEmployeeById: ดึง Employee.Id = 1  ที่มีในระบบ ต้องได้ HttpStatusCode = OK")]
        public async Task GetEmployeeById_ExistingId_ReturnsOkResponse_WithEmployee()
        {
            // Arrange
            var existingEmployeeId = 1;
            var url = $"/api/Employees/{existingEmployeeId}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var employee = await response.Content.ReadFromJsonAsync<EmployeeDto>();
            Assert.NotNull(employee);
            Assert.Equal(existingEmployeeId, employee.Id);
        }

        [Fact(DisplayName = "GetEmployeeById: ดึง Employee.Id = 9999 ที่ไม่มีในระบบ ต้องได้ HttpStatusCode = NotFound")]
        public async Task GetEmployeeById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingEmployeeId = 9999;
            var url = $"/api/Employees/{nonExistingEmployeeId}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
         
        [Fact(DisplayName = "CreateEmployee: ต้องได้ StatusCode = Created")]
        public async Task CreateEmployee_ValidEmployee_ReturnsCreatedResponse()
        {
            // Arrange
            var url = "/api/Employees";
            var newEmployeeDto = new CreateEmployeeDto
            {
                Name = "Nuchit Atjanawat",
                Position = "Developer",
                Salary = 100000
            };

            // Act
            var response = await _client.PostAsJsonAsync(url, newEmployeeDto);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
             
            var createdEmployee = await response.Content.ReadFromJsonAsync<CreateEmployeeDto>();
            Assert.NotNull(createdEmployee);

            var location = response.Headers.Location.ToString();
            Assert.Contains("/api/Employees/", location);
        }

        //UpdateEmployee Integration Test
        [Fact(DisplayName = "UpdateEmployee: Update Employee ที่มี ต้องได้ StatusCode = NoContent")]
        public async Task UpdateEmployee_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var existingEmployeeId = 1; 
            var url = $"/api/Employees/{existingEmployeeId}";
            var updatedEmployee = new UpdateEmployeeDto
            {
                Name = "Nuchit Atjanawat",
                Position = "Senior Developer",
                 Salary = 120000
            };

            // Act
            var response = await _client.PutAsJsonAsync(url, updatedEmployee);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        //DeleteEmployee Integration Test
        //[Fact(DisplayName = "DeleteEmployee Integration Test")]
        //public async Task DeleteEmployee_ExistingId_ReturnsNoContent()
        //{
        //    // Arrange
        //    var existingEmployeeId = 4; // Use a valid ID from your test data
        //    var url = $"/api/Employees/{existingEmployeeId}";

        //    // Act
        //    var response = await _client.DeleteAsync(url);

        //    // Assert
        //    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        //}

        [Fact(DisplayName = "DeleteEmployee: ลบ Employee ที่ไม่มี ต้องได้ StatusCode = NotFound")]
        public async Task DeleteEmployee_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingEmployeeId = 9999; // Use an ID that doesn't exist in test data
            var url = $"/api/Employees/{nonExistingEmployeeId}";

            // Act
            var response = await _client.DeleteAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


    }

}