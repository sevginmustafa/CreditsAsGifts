namespace CreditsAsGifts.Areas.Administration.Controllers
{
    using CreditsAsGifts.Models;
    using CreditsAsGifts.Models.Users;
    using CreditsAsGifts.Services.Users;
    using Microsoft.AspNetCore.Mvc;

    using static CreditsAsGifts.Common.GlobalConstants;

    public class UsersController : AdministrationController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult GetAll(string searchString, int page = 1)
        {
            var users = this.usersService
                .SearchUsersByPhoneNumber(searchString);

            var paginatedUsers = PaginatedList<UserAdministrationViewModel>.
                Create(users, page, UsersCountPerPage);

            var viewModel = new UserSearchViewModel
            {
                Users = paginatedUsers,
                SearchString = searchString
            };

            return this.View(viewModel);
        }
    }
}
