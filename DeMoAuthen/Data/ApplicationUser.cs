using Microsoft.AspNetCore.Identity;

namespace DeMoAuthen.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirtName { get; set; } =null!;
        public string LastName { get; set; } = null!;
    }
}
