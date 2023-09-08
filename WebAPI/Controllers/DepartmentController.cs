using Application.Dto;
using Application.Dto.Student;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [SwaggerOperation(Summary = "Retrieves all department")]
        [HttpGet]
        public IActionResult Get()
        {
            var departments = _departmentService.GetAllDepartments();
            return Ok(departments);
        }

        [SwaggerOperation(Summary = "Retrieves a specific department by unique ID")]
        [HttpGet("{Id}")]
        public IActionResult Get(int DepartmentId)
        {
            var department = _departmentService.GetDepartmentById(DepartmentId);

            if (department is null)
                return NotFound();

            return Ok(department);
        }

        [SwaggerOperation(Summary = "Create a new department")]
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto newDepartment)
        {
            var department = _departmentService.CreateDepartment(newDepartment);
            return Created($"api/students/{department.DepartmentId}", department);
        }

        [SwaggerOperation(Summary = "Update a existing department")]
        [HttpPut]
        public IActionResult Update(UpdateDepartmentDto updateDepartment)
        {
            _departmentService.UpdateDepartment(updateDepartment);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete a specific department")]
        [HttpDelete("{Id}")]
        public IActionResult Delete(int DepartmentId)
        {
            _departmentService.DeleteDepartment(DepartmentId);
            return NoContent();
        }
    }
}
