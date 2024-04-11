namespace StudentRestAPI.Models.Interface
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> Search(string name, Gender? gender);
        Task<Instructor> GetInstructor(Guid instructorId);
        Task<IEnumerable<Instructor>> GetInstructors();
        Task<Instructor> GetStudentByEmail(string email);
        Task<Instructor> AddInstructor(Instructor instructor);
        Task<Instructor> UpdateInstructor(Instructor instructor);
        Task DeleteInstructor(Guid instructorid);
    }
}
