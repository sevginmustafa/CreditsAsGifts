namespace CreditsAsGifts.Services.Users
{
    public interface IUsersService
    {
        bool IsEmailAvailable(string email);

        bool isPhoneNumberAvailable(string phoneNumber);
    }
}
