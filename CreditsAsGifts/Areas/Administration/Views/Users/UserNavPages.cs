namespace CreditsAsGifts.Areas.Administration.Views.Users
{
    using CreditsAsGifts.Areas.Administration.Views.Shared;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class UserNavPages : AdminNavPages
    {
        public static string GetAll => "GetAll";

        public static string GetAllNavClass(ViewContext viewContext) => PageNavClass(viewContext, GetAll);
    }
}
