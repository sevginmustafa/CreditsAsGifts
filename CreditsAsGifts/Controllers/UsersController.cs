namespace CreditsAsGifts.Controllers
{
    using CreditsAsGifts.Common.Enums;
    using CreditsAsGifts.Data.Models;
    using CreditsAsGifts.Infrastructure.Extensions;
    using CreditsAsGifts.Models;
    using CreditsAsGifts.Models.Gifts;
    using CreditsAsGifts.Services.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using static CreditsAsGifts.Common.GlobalConstants;

    public class UsersController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUsersService usersService;

        public UsersController(SignInManager<ApplicationUser> signInManager, IUsersService usersService)
        {
            this.signInManager = signInManager;
            this.usersService = usersService;
        }

        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await this.signInManager.SignOutAsync();

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            if (this.User.IsInRole(AdministratorRoleName))
            {
                return this.RedirectToAction("Index", "Dashboard", new { area = "Administration" });
            }

            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> MyTransactionsAsync(string searchString, int transactionType = 0, int page = 1)
        {
            this.ViewData["UserPhoneNumber"] = await this.usersService
                .GetUserPhoneNumberAsync(this.User.GetId());

            var transactions = await this.usersService
                .SearchTransactionsByPhoneNumberAsync(searchString, transactionType, this.User.GetId());

            var paginatedTransactions = PaginatedList<TransactionViewModel>.
                Create(transactions, page, TransactionsCountPerPage);

            var viewModel = new TransactionSearchViewModel
            {
                Transactions = paginatedTransactions,
                SearchString = searchString,
                TransactionType = (TransactionType)transactionType
            };

            return this.View(viewModel);
        }
    }
}
