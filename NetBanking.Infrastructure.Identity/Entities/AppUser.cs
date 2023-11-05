

namespace NetBanking.Infrastructure.Identity.Entities
{
    public class AppUser 
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string? ImagePath { get; set; } 
        public bool IsActive { get; set; } 
    }
}
