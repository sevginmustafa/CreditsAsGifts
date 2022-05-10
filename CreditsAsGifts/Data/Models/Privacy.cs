namespace CreditsAsGifts.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Privacy
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
