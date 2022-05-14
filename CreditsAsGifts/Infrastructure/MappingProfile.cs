namespace CreditsAsGifts.Infrastructure
{
    using AutoMapper;
    using CreditsAsGifts.Data.Models;
    using CreditsAsGifts.Models.Gifts;
    using CreditsAsGifts.Models.Privacy;
    using CreditsAsGifts.Models.Users;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Privacy, PrivacyViewModel>();

            this.CreateMap<TermsAndConditions, TermsAndConditionsViewModel>();

            this.CreateMap<Gift, TransactionViewModel>();

            this.CreateMap<ApplicationUser, UserAdministrationViewModel>();
        }
    }
}
