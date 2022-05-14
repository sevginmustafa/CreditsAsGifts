using CreditsAsGifts.Areas.Administration.Views.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditsAsGifts.Areas.Administration.Views.Users
{
    public class UserNavPages : AdminNavPages
    {
        public static string GetAll => "GetAll";

        public static string GetAllNavClass(ViewContext viewContext) => PageNavClass(viewContext, GetAll);
    }
}
