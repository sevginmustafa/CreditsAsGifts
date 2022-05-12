namespace CreditsAsGifts.Models.Gifts
{
    using System;

    public class TransactionViewModel
    {
        public DateTime Date { get; set; }

        public string SenderUserName { get; set; }

        public string SenderPhoneNumber { get; set; }

        public string RecipientUserName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public int NumberOfCredits { get; set; }

        public string Message { get; set; }
    }
}
