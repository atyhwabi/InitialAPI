namespace StudentRestAPI.Models.Interface
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> Search(string name, Gender? gender);
        Task<Student> GetStudent(Guid studentId);
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudentByEmail(string email);
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task DeleteStudent(Guid studentid);
    }
}
