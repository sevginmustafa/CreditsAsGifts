namespace CreditsAsGifts.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.GiftsReceived = new HashSet<GiftReceived>();
            this.GiftsSended = new HashSet<GiftSended>();
        }

        public int Credits { get; set; }

        public virtual ICollection<GiftReceived> GiftsReceived { get; set; }

        public virtual ICollection<GiftSended> GiftsSended { get; set; }
    }
}
