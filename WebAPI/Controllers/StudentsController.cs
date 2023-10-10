using Application.Dto.Student;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using System.Net;
using System.Security.Claims;
using Application.Configuration.Commands.Students.CreateStudent;
using Application.Configuration.Commands.Students.UpdateStudent;
using Application.Configuration.Queries.Students.GetAllStudents;
using Application.Configuration.Queries.Students.GetStudentById;
using Application.Configuration.Queries.Students.GetStudentByEmail;
using Application.Configuration.Commands.Students.DeleteStudent;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Wrappers;
using Infrastructure.Identity;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [SwaggerOperation(Summary = "Retrieves all students")]
        [Authorize(Roles = UserRoles.Admin)]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(ListStudentsDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Retrieves a specific student by unique ID")]
        [AllowAnonymous]
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
        [Authorize(Roles = UserRoles.User)]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create(CreateStudentCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.UserId = userId.ToString();
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
  
        }

        [SwaggerOperation(Summary = "Update a existing student")]
        [Authorize(Roles = UserRoles.User)]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(UpdateStudentCommand command)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var userOwnsPost = await _mediator.Send(new UserOwnStudentQuery(command.Id, userId));
            if (!userOwnsPost) {
                return BadRequest(new Response<bool> { Succeeded = false, Message = "You do not own this student" });
            }


            await _mediator.Send(command);
            return NoContent();
        }


        [SwaggerOperation(Summary = "Delete a specific student")]
        [Authorize(Roles = UserRoles.User)]
        [HttpDelete("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int Id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var userOwnsPost = await _mediator.Send(new UserOwnStudentQuery(Id, userId));
            var isAdmin = User.FindFirstValue(ClaimTypes.Role).Contains(UserRoles.Admin);

            if (!isAdmin && !userOwnsPost)
            {
                return BadRequest(new Response<bool> { Succeeded = false, Message = "You do not own this student" });
            }

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
