namespace CreditsAsGifts.Models.Users
{
    public class UserSearchViewModel
    {
        public PaginatedList<UserAdministrationViewModel> Users { get; set; }

        public string SearchString { get; set; }
    }
}
