

using Application.Commands.Departments.CreateDepartment;
using Application.Commands.Departments.UpdateDepartment;
using Application.Commands.Students.CreateStudent;
using Application.Commands.Students.UpdateStudent;
using Application.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(executingAssembly));

            services.AddScoped<IValidator<CreateStudentCommand>, CreateStudentCommandValidation>();
            services.AddScoped<IValidator<UpdateStudentCommand>, UpdateStudentCommandValidation>();
            services.AddScoped<IValidator<CreateDepartmentCommand>, CreateDepartmentCommandValidation>();
            services.AddScoped<IValidator<UpdateDepartmentCommand>, UpdateDepartmentCommandValidation>();

            //services.AddTransient<ExceptionHandlingMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseApplication(this WebApplication app)
        {
            //app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
