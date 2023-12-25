using Microsoft.AspNetCore.Identity;

namespace Data_Access_Tier.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
