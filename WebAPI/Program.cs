using Application.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Application.Services;
using Application.Services.Abstractions;
using Application.Validators;
using Application.Validators.Abstractions;
using Core.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Context;
using MediatR;
using FluentValidation;
using Application.Commands.Students.CreateStudent;
using Application.Commands.Students.UpdateStudent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Student Services
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
// builder.Services.AddScoped<IStudentService, StudentService>();
// builder.Services.AddScoped<IStudentValidator, StudentValidator>();

// Department Services
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentValidator, DepartmentValidator>();

// Add MediatR
var applicationAssembly = AppDomain.CurrentDomain.GetAssemblies().Single(assembly => assembly.GetName().Name == "Application");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));
builder.Services.AddScoped<IValidator<CreateStudentCommand>, CreateStudentCommandValidation>();
builder.Services.AddScoped<IValidator<UpdateStudentCommand>, UpdateStudentCommandValidation>();

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