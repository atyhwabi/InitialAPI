using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentRestAPI.Models
{
    public abstract class Person
    {
        [Key]
        public Guid PersonID { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Column("FirstName")]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Gender Gender { get; set; }

        public string PhotoPath { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public int DepartmentID { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }
        
    }
}
