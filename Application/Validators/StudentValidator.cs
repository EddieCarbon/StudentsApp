// using Application.Dto;
// using Application.Dto.Student;
// using Application.Validators.Abstractions;
// using Core.Entities;
// using Core.Repositories;
//
// namespace Application.Validators;
//
// public class StudentValidator : IStudentValidator
// {
//     private readonly IStudentRepository _studentRepository;
//
//     public StudentValidator(IStudentRepository studentRepository)
//     {
//         _studentRepository = studentRepository;
//     }
//
//     public void Validate(CreateStudentDto student)
//     {
//         ValidateName(student.Name);
//         ValidateSurname(student.Surname);
//         ValidateEmail(student.Email);
//         ValidateDateOfBirth(student.DateOfBirth);
//         ValidateStartingStudyYear(student.StartingStudyYear);
//     }
//
//     public void Validate(UpdateStudentDto student)
//     {
//         IsExist(student.Id);
//         ValidateName(student.Name);
//         ValidateSurname(student.Surname);
//         ValidateEmail(student.Email);
//         ValidateDateOfBirth(student.DateOfBirth);
//         ValidateStartingStudyYear(student.StartingStudyYear);
//     }
//
//     public void Validate(int Id)
//     {
//         IsExist(Id);
//     }
//
//     private void IsExist(int Id)
//     {
//         var student = _studentRepository.GetById(Id);
//
//         if (student is null)
//         {
//             throw new Exception($"Student with ID {Id} does not exist.");
//         }
//     }
//
//     private void ValidateName(string name)
//     {
//         if (string.IsNullOrEmpty(name))
//         {
//             throw new Exception("Student can not have an empty name.");
//         }
//
//         if (name.Length > 20)
//         {
//             throw new Exception("Student name can be 20 characters at most.");
//         }
//     }
//
//     private void ValidateSurname(string surname)
//     {
//         if (string.IsNullOrEmpty(surname))
//         {
//             throw new Exception("Surname can not have an empty name.");
//         }
//
//         if (surname.Length > 40)
//         {
//             throw new Exception("Surname name can be 40 characters at most.");
//         }
//     }
//
//     private void ValidateEmail(string email)
//     {
//         if (email is null)
//         {
//             return;
//         }
//     }
//     private void ValidateDateOfBirth(DateTime date)
//     {
//
//     }
//
//     private void ValidateStartingStudyYear(int studyYear)
//     {
//         if (studyYear == 0)
//         {
//             return;
//         }
//     }
//
// }
