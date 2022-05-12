namespace CreditsAsGifts.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public int Credits { get; set; }
    }
}
