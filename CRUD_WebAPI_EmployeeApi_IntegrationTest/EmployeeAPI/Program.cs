using EmployeeApi.Data;
using EmployeeApi.Repository;
using EmployeeApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Add AutoMapper
builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>(); // Add SQL Data Access
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Add Repository
builder.Services.AddScoped<IEmployeeService, EmployeeService>(); // Add Service

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SQL Server connection string
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
// Enable middleware to serve Swagger UI, specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

app.MapControllers();

app.Run();
