namespace CreditsAsGifts.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GiftSended
    {
        public int Id { get; set; }

        public int NumberOfCredits { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public virtual ApplicationUser Recipient { get; set; }
    }
}
