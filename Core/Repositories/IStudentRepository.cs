using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Repositories;

public interface IStudentRepository
{
    Student GetById(int Id);
    IQueryable<Student> GetAll();
    Student Add(Student student);
    void Update(Student student);
    void Delete(Student student);
}
