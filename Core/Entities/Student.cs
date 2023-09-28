using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;
public class Student
{
    public int Id { get; set; }
    [Required]
    [MaxLength(450)]
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int StartingStudyYear { get; set; }

    public StudentAddress Address { get; set; }
    public int CurrentDepartmentId { get; set; }
    public Department Department { get; set; }
    /*public IList<StudentCourse> StudentCourses { get; set; }*/
}




