namespace CreditsAsGifts.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static CreditsAsGifts.Data.DataValidation;

    public class Gift
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string From { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string To { get; set; }

        public int NumberOfCredits { get; set; }

        public string Message { get; set; }

    }
}
