using Microsoft.EntityFrameworkCore;
using StudentRestAPI.Models.Interface;

namespace StudentRestAPI.Models.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AppDBContext _appDBContext;

        public InstructorRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public async Task<Instructor> AddInstructor(Instructor instructor)
        {
            try
            {
                var result = await _appDBContext.Instructors.AddAsync(instructor);
                await _appDBContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task DeleteStudent(Guid instructorid)
        {
            var result = await _appDBContext.Instructors
                .FirstOrDefaultAsync(e => e.PersonID == instructorid);
            if (result != null)
            {
                _appDBContext.Instructors.Remove(result);
                await _appDBContext.SaveChangesAsync();
            }
        }
        public async Task<Instructor?> GetStudent(Guid instructorId)
        {
            return await _appDBContext.Instructors
                .FirstOrDefaultAsync(e => e.PersonID == instructorId);
        }
        public async Task<Instructor?> GetStudentByEmail(string email)
        {
            return await _appDBContext.Instructors
                .FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<IEnumerable<Instructor>> GetInstructors()
        {
            return await _appDBContext.Instructors.ToListAsync();
        }
        public async Task<IEnumerable<Instructor>> Search(string name, Gender? gender)
        {
            IQueryable<Instructor> query = _appDBContext.Instructors;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(
                    e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }
            if (gender !=  null)
            {
                query = query.Where(
                e => e.Gender == gender);
            }
            return await query.ToListAsync();

        }
        public async Task<Instructor?> UpdateStudent(Instructor instructor)
        {

            var result = await _appDBContext.Instructors
                .FirstOrDefaultAsync(e => e.PersonID == instructor.PersonID);

            if (result != null)
            {
                result.FirstName = instructor.FirstName;
                result.LastName = instructor.LastName;
                result.Email = instructor.Email;
                result.Phone = instructor.Phone;
                result.Gender = instructor.Gender;
                result.PhotoPath = instructor.PhotoPath;
                result.InstructorNumber = instructor.InstructorNumber;
                result.HireDate = instructor.HireDate;
                result.DateOfBirth = instructor.DateOfBirth;
                await _appDBContext.SaveChangesAsync();

                return result;
            }
            return null;
        }
        public async Task DeleteInstructor(Guid instructorid)
        {
            var result = await _appDBContext.Instructors
                .FirstOrDefaultAsync(e => e.PersonID == instructorid);
            if (result != null)
            {
                _appDBContext.Instructors.Remove(result);
                await _appDBContext.SaveChangesAsync();
            }
        }
        public async Task<Instructor?> GetInstructor(Guid instructorId)
        {
            return await _appDBContext.Instructors
                .FirstOrDefaultAsync(e => e.PersonID == instructorId);
        }
        public async Task<Instructor?> GetInstructorByEmail(string email)
        {
            return await _appDBContext.Instructors
                .FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<Instructor?> UpdateInstructor(Instructor instructor)
        {

            var result = await _appDBContext.Instructors
                .FirstOrDefaultAsync(e => e.PersonID == instructor.PersonID);

            if (result != null)
            {
                result.FirstName = instructor.FirstName;
                result.LastName = instructor.LastName;
                result.Email = instructor.Email;
                result.Phone = instructor.Phone;
                result.Gender = instructor.Gender;
                result.DepartmentID = instructor.DepartmentID;
                result.PhotoPath = instructor.PhotoPath;
                result.DateOfBirth = instructor.DateOfBirth;
                result.InstructorNumber = instructor.InstructorNumber;
                await _appDBContext.SaveChangesAsync();

                return result;
            }
            return null;
        }


    }
}
