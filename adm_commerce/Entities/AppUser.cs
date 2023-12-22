using Microsoft.AspNetCore.Identity;

namespace adm_commerce.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }

    }
}
