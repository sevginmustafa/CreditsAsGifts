namespace CreditsAsGifts.Services.Privacy
{
    using CreditsAsGifts.Models.Privacy;
    using System.Threading.Tasks;

    public interface IPrivacyService
    {
        public Task<PrivacyViewModel> GetPrivacyAsync();

        public Task<TermsAndConditionsViewModel> GetTermsAndConditionsAsync();
    }
}
