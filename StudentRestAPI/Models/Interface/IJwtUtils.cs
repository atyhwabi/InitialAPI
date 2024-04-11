using StudentRestAPI.Models.Admin;

namespace StudentRestAPI.Models.Interface
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public int? ValidateToken(string token);
    }
}
