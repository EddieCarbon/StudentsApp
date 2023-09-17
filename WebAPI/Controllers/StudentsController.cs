using Application.Dto.Student;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using System.Net;
using Application.Commands.Students.DeleteStudent;
using Application.Queries.Students.GetAllStudents;
using Application.Queries.Students.GetStudentByEmail;
using Application.Queries.Students.GetStudentById;
using Application.Configuration.Commands.Students.CreateStudent;
using Application.Configuration.Commands.Students.UpdateStudent;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [SwaggerOperation(Summary = "Retrieves all students")]
        [HttpGet]
        [ProducesResponseType(typeof(ListStudentsDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Retrieves a specific student by unique ID")]
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(StudentDetailDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery(Id));
            return result != null ? Ok(result) : NotFound();
        }

        [SwaggerOperation(Summary = "Retrieves a specific student by unique Email")]
        [HttpGet("[action]/{Email}")]
        [ProducesResponseType(typeof(StudentDetailDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string Email)
        {
            var result = await _mediator.Send(new GetStudentByEmailQuery(Email));
            return result != null ? Ok(result) : NotFound();
        }

        [SwaggerOperation(Summary = "Create a new student")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create(CreateStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [SwaggerOperation(Summary = "Update a existing student")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(UpdateStudentCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [SwaggerOperation(Summary = "Delete a specific student")]
        [HttpDelete("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int Id)
        {
            await _mediator.Send(new DeleteStudentCommand(Id));
            return NoContent();
        }
        
        #region Old Solution

        // private readonly IStudentService _studentService;
        
        // public StudentsController(IStudentService studentService)
        // {
        //    _studentService = studentService;
        // }
        
        // public IActionResult Get()
        // {
        //     var students = _studentService.GetAllStudents();
        //     return Ok(students);
        // }
        
        // public IActionResult Get(int Id)
        // {
        //     var student = _studentService.GetStudentById(Id);

        //     if (student is null)
        //         return NotFound();

        //     return Ok(student);
        // }
        
        //[SwaggerOperation(Summary = "Retrieves a specific student by unique Email")]
        //[HttpGet("[action]/{Email}")]
        //public IActionResult Get(string Email)
        //{
        //    var student = _studentService.GetStudentByEmail(Email);

        //    if (student is null)
        //        return NotFound();

        //    return Ok(student);
        //}
        
        // public IActionResult Create(CreateStudentDto newStudent)
        // {
        //     var student = _studentService.CreateStudent(newStudent);
        //     return Created($"api/students/{student.Id}", student);
        // }
        
        // public IActionResult Update(UpdateStudentDto updateStudent)
        // {
        //     _studentService.UpdateStudent(updateStudent);
        //     return NoContent();
        // }
        
        // [SwaggerOperation(Summary = "Delete a specific student")]
        // [HttpDelete("{Id}")]
        // public IActionResult Delete(int Id)
        // {
        //     _studentService.DeleteStudent(Id);
        //     return NoContent();
        // }

        #endregion
    }
}
