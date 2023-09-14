using Application.Commands.Departments.CreateDepartment;
using Application.Commands.Departments.UpdateDepartment;
using Application.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Core.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Context;
using Infrastructure;
using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();

// Core Services
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Add Mediator
var applicationAssembly = AppDomain.CurrentDomain.GetAssemblies().Single(assembly => assembly.GetName().Name == "Application");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));

// Add DB Context
builder.Services.AddDbContext<StudentAppContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("StudentCS")));

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