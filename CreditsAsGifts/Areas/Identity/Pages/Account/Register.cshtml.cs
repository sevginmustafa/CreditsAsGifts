using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CreditsAsGifts.Data.Models;
using CreditsAsGifts.Services.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

using static CreditsAsGifts.Common.GlobalConstants;

namespace CreditsAsGifts.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IUsersService usersService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IUsersService usersService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.usersService = usersService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Display(Name = "Username")]
            [Required(ErrorMessage = "Username is required!")]
            [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long!", MinimumLength = 3)]
            public string UserName { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "Email address is required!")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address!")]
            public string Email { get; set; }

            [Display(Name = "Phone number")]
            [Required(ErrorMessage = "Phone number is required!")]
            [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number!")]
            [RegularExpression(@"^(0[0-9]{9})$", ErrorMessage = "Invalid phone number! The phone number should start with 0 and should be 10 digits long!")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Password")]
            [Required(ErrorMessage = "Password is required!")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long!", MinimumLength = 6)]
            public string Password { get; set; }

            [Display(Name = "Confirm password")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match!")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                if (!this.usersService.IsEmailAvailable(this.Input.Email))
                {
                    this.ModelState.AddModelError("Email", $"Email address '{this.Input.Email}' is already taken.");
                }
                if (!this.usersService.isPhoneNumberAvailable(this.Input.PhoneNumber))
                {
                    this.ModelState.AddModelError("PhoneNumber", $"Phone number '{this.Input.PhoneNumber}' is already taken.");
                }
                else
                {
                    var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email, PhoneNumber = Input.PhoneNumber, Credits = 100 };
                    var result = await userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, UserRoleName);

                        logger.LogInformation("User created a new account with password.");

                        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            await signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
