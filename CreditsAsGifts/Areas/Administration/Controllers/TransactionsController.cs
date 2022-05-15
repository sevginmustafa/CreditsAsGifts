namespace CreditsAsGifts.Areas.Administration.Controllers
{
    using CreditsAsGifts.Infrastructure.Extensions;
    using CreditsAsGifts.Models;
    using CreditsAsGifts.Models.Gifts;
    using CreditsAsGifts.Services.Users;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using static CreditsAsGifts.Common.GlobalConstants;

    public class TransactionsController : AdministrationController
    {
        private readonly IUsersService usersService;

        public TransactionsController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> GetAllAsync(string searchString, int page = 1)
        {
            var transactions = await this.usersService
                .SearchTransactionsByPhoneNumberAsync(searchString);

            var paginatedTransactions = PaginatedList<TransactionViewModel>.
                Create(transactions, page, TransactionsCountPerPage);

            var viewModel = new TransactionSearchViewModel
            {
                Transactions = paginatedTransactions,
                SearchString = searchString
            };

            return this.View(viewModel);
        }
    }
}
