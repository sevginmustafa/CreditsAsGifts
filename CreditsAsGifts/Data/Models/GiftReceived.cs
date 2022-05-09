namespace CreditsAsGifts.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GiftReceived
    {
        public int Id { get; set; }

        public int NumberOfCredits { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }
    }
}
