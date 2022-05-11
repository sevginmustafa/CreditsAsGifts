namespace CreditsAsGifts.Services.Privacy
{
    using CreditsAsGifts.Data;
    using CreditsAsGifts.Models.Privacy;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using System.Threading.Tasks;

    public class PrivacyService : IPrivacyService
    {
        private readonly CreditsAsGiftsDbContext database;
        private readonly IMapper mapper;

        public PrivacyService(CreditsAsGiftsDbContext database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public async Task<PrivacyViewModel> GetPrivacyAsync()
            => mapper.Map<PrivacyViewModel>( await this.database.Privacies.FirstOrDefaultAsync());

        public async Task<TermsAndConditionsViewModel> GetTermsAndConditionsAsync()
             => mapper.Map<TermsAndConditionsViewModel>(await this.database.TermsAndConditions.FirstOrDefaultAsync());
    }
}
