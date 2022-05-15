namespace CreditsAsGifts.Models.Gifts
{
    using Ganss.XSS;
    using System;

    public class TransactionViewModel
    {
        public DateTime Date { get; set; }

        public string SenderName { get; set; }

        public string SenderPhoneNumber { get; set; }

        public string RecipientName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public int NumberOfCredits { get; set; }

        public string Message { get; set; }
    }
}
