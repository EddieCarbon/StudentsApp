using Core.Entities;

namespace Core.Repositories;

public interface IStudentRepository
{
    Student GetById(int Id);
    IQueryable<Student> GetAll();
    Task<bool> IsAlreadyExistAsync(string email, CancellationToken cancellation = default);
    Student Add(Student student);
    void Update(Student student);
    void Delete(Student student);
}
