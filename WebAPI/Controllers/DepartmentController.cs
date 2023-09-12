using System.Net;
using Application.Commands.Departments.CreateDepartment;
using Application.Commands.Departments.DeleteDepartment;
using Application.Commands.Departments.UpdateDepartment;
using Application.Dto.Student;
using Application.Queries.Departments.GetAllDepartments;
using Application.Queries.Departments.GetDepartmentById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [SwaggerOperation(Summary = "Retrieves all department")]
        [HttpGet]
        [ProducesResponseType(typeof(ListDepartmentsDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllDepartmentsQuery());
            return Ok(result);
        }
        
        [SwaggerOperation(Summary = "Retrieves a specific department by unique ID")]
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(DepartmentDetailDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int departmentId)
        {
            var result = await _mediator.Send(new GetDepartmentByIdQuery(departmentId));
            return result != null ? Ok(result) : NotFound();
        }
        
        [SwaggerOperation(Summary = "Create a new department")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create(CreateDepartmentCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.DepartmentId }, result);
        }
        
        [SwaggerOperation(Summary = "Update a existing department")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(UpdateDepartmentCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        
        [SwaggerOperation(Summary = "Delete a specific department")]
        [HttpDelete("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int departmentId)
        {
            await _mediator.Send(new DeleteDepartmentCommand(departmentId));
            return NoContent();
        }

        #region Old Solution
        
        // private readonly IDepartmentService _departmentService;
        //
        // public DepartmentController(IDepartmentService departmentService)
        // {
        //     _departmentService = departmentService;
        // }
        
        // [SwaggerOperation(Summary = "Retrieves all department")]
        // [HttpGet]
        // public IActionResult Get()
        // {
        //     var departments = _departmentService.GetAllDepartments();
        //     return Ok(departments);
        // }
        //
        // [SwaggerOperation(Summary = "Retrieves a specific department by unique ID")]
        // [HttpGet("{Id}")]
        // public IActionResult Get(int DepartmentId)
        // {
        //     var department = _departmentService.GetDepartmentById(DepartmentId);
        //
        //     if (department is null)
        //         return NotFound();
        //
        //     return Ok(department);
        // }
        // [SwaggerOperation(Summary = "Create a new department")]
        // [HttpPost]
        // public IActionResult Create(CreateDepartmentDto newDepartment)
        // {
        //     var department = _departmentService.CreateDepartment(newDepartment);
        //     return Created($"api/students/{department.DepartmentId}", department);
        // }
        // [SwaggerOperation(Summary = "Update a existing department")]
        // [HttpPut]
        // public IActionResult Update(UpdateDepartmentDto updateDepartment)
        // {
        //     _departmentService.UpdateDepartment(updateDepartment);
        //     return NoContent();
        // }
        // [SwaggerOperation(Summary = "Delete a specific department")]
        // [HttpDelete("{Id}")]
        // public IActionResult Delete(int DepartmentId)
        // {
        //     _departmentService.DeleteDepartment(DepartmentId);
        //     return NoContent();
        // }

        #endregion
    }
}
