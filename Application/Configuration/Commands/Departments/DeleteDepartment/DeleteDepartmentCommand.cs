using MediatR;

namespace Application.Configuration.Commands.Departments.DeleteDepartment;

public record DeleteDepartmentCommand(int Id) : IRequest { }