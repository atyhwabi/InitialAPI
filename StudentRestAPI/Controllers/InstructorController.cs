using Microsoft.AspNetCore.Mvc;
using StudentRestAPI.Models.Interface;
using StudentRestAPI.Models;

namespace StudentRestAPI.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository _InstructorRepository;

        public InstructorController(IInstructorRepository instructorRepository)
        {
            this._InstructorRepository = instructorRepository;
        }
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Instructor>>> Search(string name, Gender? gender)
        {
            try
            {
                var result = await _InstructorRepository.Search(name, gender);
                if (result.Any())
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
        [HttpGet("{instructorId}")]
        public async Task<ActionResult<Instructor>> GetInstructor(Guid studentId)
        {
            try
            {
                var result = await _InstructorRepository.GetInstructor(studentId);
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
        public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructors()
        {
            try
            {
                var result = await _InstructorRepository.GetInstructors();
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
        [HttpPost("Add")]
        public async Task<ActionResult<Instructor>> AddInstructor(Instructor instructor)
        {
            try
            {
                if (instructor == null)
                {
                    return BadRequest("Instructor entity was null");
                }
                var resultCheck = await _InstructorRepository.GetStudentByEmail(instructor.Email);
                if (resultCheck != null)
                {
                    return BadRequest("Instructor with this email already exists");
                }
                var result = await _InstructorRepository.AddInstructor(instructor);
                return CreatedAtAction(nameof(GetInstructor), new { id = result.PersonID }, result);
            }
            catch (Exception)
            {
                return BadRequest("Error adding data to the database");
            }
        }
        [HttpPut("id:int")]
        public async Task<ActionResult<Instructor>> UpdateInstructor(Guid id, Instructor instructor)
        {
            try
            {
                if (id != instructor.PersonID)
                {
                    return BadRequest("Student ID mismatch");
                }

                var studentToUpdate = await _InstructorRepository.GetInstructor(id);
                if (studentToUpdate == null)
                {
                    return NotFound($"Instructor with ID = {id} not found");
                }
                return await _InstructorRepository.UpdateInstructor(instructor);
            }
            catch (Exception)
            {
                return BadRequest("Error updating data to the database");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Instructor>> DeleteStudent(Guid id)
        {
            try
            {
                var result = await _InstructorRepository.GetInstructor(id);
                if (result == null)
                {
                    return NotFound($"Student with ID = {id} not found");
                }
                await _InstructorRepository.DeleteInstructor(id);
                return Ok($"Instructor with ID = {id} deleted");
            }
            catch (Exception)
            {
                return BadRequest("Error deleting data from the database");
            }
        }
    }
}
