using Microsoft.AspNetCore.Mvc;
using StudentRestAPI.Models;
using StudentRestAPI.Models.Interface;

namespace StudentRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }
        //[HttpGet("{search}")]
        //public async Task<ActionResult<IEnumerable<Student>>> Search(string name, Gender? gender)
        //{
        //    try
        //    {
        //        var result = await _studentRepository.Search(name, gender);
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }
        //        return NotFound();
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest("Error retrieving data from the database");
        //    }
        //}
        [HttpGet("{studentId}")]
        public async Task<ActionResult<Student>> GetStudent(Guid studentId)
        {
            try
            {
                var result = await _studentRepository.GetStudent(studentId);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {
                var result = await _studentRepository.GetStudents();
                if (result.Any())
                {
                    return Ok(result.ToList());
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    return BadRequest("Student entity was null");
                }
                var resultCheck = await _studentRepository.GetStudentByEmail(student.Email);
                if (resultCheck != null)
                {
                    return BadRequest("Student with this email already exists");
                }
                var result = await _studentRepository.AddStudent(student);
                return CreatedAtAction(nameof(GetStudent), new { id = result.PersonID }, result);
            }
            catch (Exception)
            {
                return BadRequest("Error adding data to the database");
            }
        }
        [HttpPut("id:int")]
        public async Task<ActionResult<Student>> UpdateStudent(Guid id, Student student)
        {
            try
            {
                if (id != student.PersonID)
                {
                    return BadRequest("Student ID mismatch");
                }

                var studentToUpdate = await _studentRepository.GetStudent(id);
                if (studentToUpdate == null)
                {
                    return NotFound($"Student with ID = {id} not found");
                }
                return await _studentRepository.UpdateStudent(student);
            }
            catch (Exception)
            {
                return BadRequest("Error updating data to the database");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Student>> DeleteStudent(Guid id)
        {
            try
            {
                var result = await _studentRepository.GetStudent(id);
                if (result == null)
                {
                    return NotFound($"Student with ID = {id} not found");
                }
                await _studentRepository.DeleteStudent(id);
                return Ok($"Student with ID = {id} deleted");
            }
            catch (Exception)
            {
                return BadRequest("Error deleting data from the database");
            }
        }   

    }
}
