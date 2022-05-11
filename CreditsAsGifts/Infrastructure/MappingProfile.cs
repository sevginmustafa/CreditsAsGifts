namespace CreditsAsGifts.Infrastructure
{
    using AutoMapper;
    using CreditsAsGifts.Data.Models;
    using CreditsAsGifts.Models.Privacy;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Privacy, PrivacyViewModel>();

            this.CreateMap<TermsAndConditions, TermsAndConditionsViewModel>();
        }
    }
}
