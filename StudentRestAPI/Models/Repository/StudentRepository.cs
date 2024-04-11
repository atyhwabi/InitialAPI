using Microsoft.EntityFrameworkCore;
using StudentRestAPI.Models.Interface;

namespace StudentRestAPI.Models.Repository
{
    public class StudentRepository: IStudentRepository
    {
        private readonly AppDBContext _appDBContext;

        public StudentRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public async Task<Student> AddStudent(Student student)
        {
            try
            {
                student.PersonID = Guid.NewGuid();
                var result = await _appDBContext.Students.AddAsync(student);
                await _appDBContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
    
        }
        public async Task DeleteStudent(Guid studentid)
        {
            var result = await _appDBContext.Students
                .FirstOrDefaultAsync(e => e.PersonID == studentid);
            if (result != null)
            {
                _appDBContext.Students.Remove(result);
                await _appDBContext.SaveChangesAsync();
            }
        }
        public async Task<Student?> GetStudent(Guid studentId)
        {
            return await _appDBContext.Students
                .FirstOrDefaultAsync(e => e.PersonID == studentId);
        }
        public async Task<Student?> GetStudentByEmail(string email)
        {
            return await _appDBContext.Students
                .FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _appDBContext.Students.ToListAsync();
        }
        public async Task<IEnumerable<Student>> Search(string name, Gender? gender)
        {
            IQueryable<Student> query = _appDBContext.Students;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(
                    e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }
            if(gender !=  null)
            {
                query = query.Where(
                e => e.Gender == gender);
            }
            return await query.ToListAsync();

        }
        public async Task<Student?> UpdateStudent(Student student)
        {

            var result = await _appDBContext.Students
                .FirstOrDefaultAsync(e => e.PersonID == student.PersonID);

            if (result != null)
            {
                result.FirstName = student.FirstName;
                result.LastName = student.LastName;
                result.Email = student.Email;
                result.Phone = student.Phone;
                result.Gender = student.Gender;
                result.DepartmentID = student.DepartmentID;
                result.PhotoPath = student.PhotoPath;
                result.DateOfBirth = student.DateOfBirth;
                result.StudentNumber = student.StudentNumber;
                await _appDBContext.SaveChangesAsync();

                return result;
            }
            return null;
        }
       
      
    }
}
