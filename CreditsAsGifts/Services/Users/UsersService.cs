namespace CreditsAsGifts.Services.Users
{
    using CreditsAsGifts.Data;
    using System.Linq;

    public class UsersService : IUsersService
    {
        private readonly CreditsAsGiftsDbContext database;

        public UsersService(CreditsAsGiftsDbContext database)
        {
            this.database = database;
        }

        public bool IsEmailAvailable(string email)
            => !this.database.ApplicationUsers.Any(x => x.Email.ToLower() == email.ToLower());

        public bool isPhoneNumberAvailable(string phoneNumber)
            => !this.database.ApplicationUsers.Any(x => x.PhoneNumber == phoneNumber);
    }
}
