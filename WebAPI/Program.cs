using Application.Commands.Departments.CreateDepartment;
using Application.Commands.Departments.UpdateDepartment;
using Application.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Core.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Context;
using FluentValidation;
using Application.Commands.Students.CreateStudent;
using Application.Commands.Students.UpdateStudent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Core Services
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Add Mediator
var applicationAssembly = AppDomain.CurrentDomain.GetAssemblies().Single(assembly => assembly.GetName().Name == "Application");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));
builder.Services.AddScoped<IValidator<CreateStudentCommand>, CreateStudentCommandValidation>();
builder.Services.AddScoped<IValidator<CreateDepartmentCommand>, CreateDepartmentCommandValidation>();
builder.Services.AddScoped<IValidator<UpdateStudentCommand>, UpdateStudentCommandValidation>();
builder.Services.AddScoped<IValidator<UpdateDepartmentCommand>, UpdateDepartmentCommandValidation>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();