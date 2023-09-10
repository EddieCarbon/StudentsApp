using MediatR;

namespace Application.Commands.Departments.DeleteDepartment;

public record DeleteDepartmentCommand(int Id) : IRequest { }