namespace Application.Dto.Student;

public class ListStudentsDto
{
    public int Count { get; set; }
    public IEnumerable<StudentDto> Students { get; set; }
}
