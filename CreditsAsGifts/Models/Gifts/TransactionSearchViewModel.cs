namespace CreditsAsGifts.Models.Gifts
{
    using CreditsAsGifts.Common.Enums;

    public class TransactionSearchViewModel
    {
        public PaginatedList<TransactionViewModel> Transactions { get; set; }

        public string SearchString { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
