
namespace NetBanking.Core.Application.Dtos.User
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Cedula { get; set; }
        public string? ProfileImagePath { get; set; }
        public bool IsActive { get; set; }
    }
}
