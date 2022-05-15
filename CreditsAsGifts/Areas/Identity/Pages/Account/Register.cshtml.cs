namespace CreditsAsGifts.Areas.Identity.Pages.Account
{
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
    using static CreditsAsGifts.Common.ModelValidation;

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
            [Display(Name = FirstNameDisplayName)]
            [Required(ErrorMessage = FirstNameRequiredMessage)]
            [StringLength(FirstNameMaxLength, ErrorMessage = FirstNameLengthErrorMessage, MinimumLength = FirstNameMinLength)]
            public string FirstName { get; set; }

            [Display(Name = LastNameDisplayName)]
            [Required(ErrorMessage = LastNameRequiredMessage)]
            [StringLength(LastNameMaxLength, ErrorMessage = LastNameLengthErrorMessage, MinimumLength = LastNameMinLength)]
            public string LastName { get; set; }

            [Display(Name = UsernameDisplayName)]
            [Required(ErrorMessage = UsernameRequiredMessage)]
            [StringLength(UsernameMaxLength, ErrorMessage = UsernameLengthErrorMessage, MinimumLength = UsernameMinLength)]
            public string UserName { get; set; }

            [Required(ErrorMessage = EmailRequiredMessage)]
            [DataType(DataType.EmailAddress, ErrorMessage = EmailInvalidErrorMessage)]
            public string Email { get; set; }

            [Display(Name = PhoneNumberDisplayName)]
            [Required(ErrorMessage = PhoneNumberRequiredMessage)]
            [DataType(DataType.PhoneNumber, ErrorMessage = PhoneNumberInvalidErrorMessage)]
            [RegularExpression(@"^(0[0-9]{9})$", ErrorMessage = PhoneNumberLengthErrorMessage)]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = PasswordRequiredMessage)]
            [DataType(DataType.Password)]
            [StringLength(PasswordMaxLength, ErrorMessage = PasswordLengthErrorMessage, MinimumLength = PasswordMinLength)]
            public string Password { get; set; }

            [Display(Name = ConfirmPasswordDisplayName)]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = PasswordCompareErrorMessage)]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Users/Dashboard");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                if (!this.usersService.IsEmailAvailable(this.Input.Email))
                {
                    this.ModelState.AddModelError("Email", $"Email address '{this.Input.Email}' is already taken.");
                }
                if (!this.usersService.IsPhoneNumberAvailable(this.Input.PhoneNumber))
                {
                    this.ModelState.AddModelError("PhoneNumber", $"Phone number '{this.Input.PhoneNumber}' is already taken.");
                }
                else
                {
                    var user = new ApplicationUser
                    {
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        UserName = Input.UserName,
                        Email = Input.Email,
                        PhoneNumber = Input.PhoneNumber,
                        Credits = 100
                    };

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
