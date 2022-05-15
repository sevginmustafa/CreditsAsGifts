using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CreditsAsGifts.Areas.Identity.IdentityHostingStartup))]
namespace CreditsAsGifts.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}