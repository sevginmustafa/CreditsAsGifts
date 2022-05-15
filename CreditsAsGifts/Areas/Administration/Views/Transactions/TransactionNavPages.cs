namespace CreditsAsGifts.Areas.Administration.Views.Transactions
{
    using CreditsAsGifts.Areas.Administration.Views.Shared;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class TransactionNavPages : AdminNavPages
    {
        public static string GetAll => "GetAll";

        public static string GetAllNavClass(ViewContext viewContext) => PageNavClass(viewContext, GetAll);
    }
}
