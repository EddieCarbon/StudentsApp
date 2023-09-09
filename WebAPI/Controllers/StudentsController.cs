﻿using Application.Dto;
using Application.Dto.Student;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using System.Net;
using Application.Commands.Students.CreateStudent;
using Application.Commands.Students.DeleteStudent;
using Application.Commands.Students.UpdateStudent;
using Application.Queries.Students.GetAllStudents;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // private readonly IStudentService _studentService;
        private readonly IMediator _mediator;
        
        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // public StudentsController(IStudentService studentService)
        // {
        //    _studentService = studentService;
        // }

        [SwaggerOperation(Summary = "Retrieves all students")]
        [HttpGet]
        [ProducesResponseType(typeof(ListStudentsDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());
            return Ok(result);
        }
        // public IActionResult Get()
        // {
        //     var students = _studentService.GetAllStudents();
        //     return Ok(students);
        // }

        [SwaggerOperation(Summary = "Retrieves a specific student by unique ID")]
        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var student = _studentService.GetStudentById(Id);

            if (student is null)
                return NotFound();

            return Ok(student);
        }

        [SwaggerOperation(Summary = "Retrieves a specific student by unique Email")]
        [HttpGet("[action]/{Email}")]
        public IActionResult Get(string Email)
        {
            var student = _studentService.GetStudentByEmail(Email);

            if (student is null)
                return NotFound();

            return Ok(student);
        }

        [SwaggerOperation(Summary = "Create a new student")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create(CreateStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        // public IActionResult Create(CreateStudentDto newStudent)
        // {
        //     var student = _studentService.CreateStudent(newStudent);
        //     return Created($"api/students/{student.Id}", student);
        // }

        [SwaggerOperation(Summary = "Update a existing student")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(UpdateStudentCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        // public IActionResult Update(UpdateStudentDto updateStudent)
        // {
        //     _studentService.UpdateStudent(updateStudent);
        //     return NoContent();
        // }

        [SwaggerOperation(Summary = "Delete a specific student")]
        [HttpDelete("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int Id)
        {
            await _mediator.Send(new DeleteStudentCommand(Id));
            return NoContent();
        }
        // [SwaggerOperation(Summary = "Delete a specific student")]
        // [HttpDelete("{Id}")]
        // public IActionResult Delete(int Id)
        // {
        //     _studentService.DeleteStudent(Id);
        //     return NoContent();
        // }
    }
}
