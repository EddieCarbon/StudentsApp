using Core.Entities;

namespace Core.Repositories;

public interface IStudentRepository
{
    Student GetById(int Id);
    IEnumerable<Student> GetAll();
    Task<bool> IsAlreadyExistAsync(string email, CancellationToken cancellation = default);
    Student Add(Student student, string userId);
    void Update(Student student);
    void Delete(Student student);
}
