namespace CreditsAsGifts.Models.Privacy
{
    using Ganss.XSS;

    public class TermsAndConditionsViewModel
    {
        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);
    }
}
