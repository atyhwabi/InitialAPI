using System.ComponentModel.DataAnnotations;

namespace StudentRestAPI.Models
{

    public class Instructor : Person
    {
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        public string InstructorNumber { get; set; }


        //public virtual ICollection<Course> Courses { get; set; }
        //public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}
    
