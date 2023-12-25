using Microsoft.AspNetCore.Identity;

namespace Data_Access_Tier.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAgree { get; set; }
    }
}
