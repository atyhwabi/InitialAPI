using System.ComponentModel.DataAnnotations;

namespace StudentRestAPI.Models.Admin
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
