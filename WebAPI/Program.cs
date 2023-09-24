using Microsoft.OpenApi.Models;
using Infrastructure;
using Application;
using Application.Configuration.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();

// Core Services
// builder.Services.AddScoped<IStudentRepository, StudentRepository>();
// builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Add Mediator
var applicationAssembly = AppDomain.CurrentDomain.GetAssemblies().Single(assembly => assembly.GetName().Name == "Application");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));

// Add DB Context
//builder.Services.AddDbContext<StudentAppContext>(options =>
//   options.UseSqlServer(builder.Configuration.GetConnectionString("StudentCS")));

// Add AutoMapper
builder.Services.AddSingleton(AutoMapperConfig.Initialize());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductsApp API", Version = "v1" });
});

builder.Host.UseInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApplication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }