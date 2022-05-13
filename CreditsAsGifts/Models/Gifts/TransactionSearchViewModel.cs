namespace CreditsAsGifts.Models.Gifts
{
    public class TransactionSearchViewModel
    {
        public PaginatedList<TransactionViewModel> Transactions { get; set; }

        public string SearchString { get; set; }
    }
}
