namespace CreditsAsGifts.Areas.Administration.Views.Shared
{
    using System;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AdminNavPages
    {
        public static string Users => "Users";

        public static string UserNavClass(ViewContext viewContext) => PageNavClass(viewContext, Users);

        protected static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}