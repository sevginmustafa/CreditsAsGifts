namespace CreditsAsGifts.Models.Gifts
{
    using System.ComponentModel.DataAnnotations;

    using static CreditsAsGifts.Common.ModelValidation;

    public class GiftSendInputModel
    {
        [Display(Name = RecipientPhoneNumberDisplayName)]
        [Required(ErrorMessage = PhoneNumberRequiredMessage)]
        [DataType(DataType.PhoneNumber, ErrorMessage = PhoneNumberInvalidErrorMessage)]
        [RegularExpression(@"^(0[0-9]{9})$", ErrorMessage = PhoneNumberLengthErrorMessage)]
        public string PhoneNumber { get; set; }

        [Display(Name = NumberOfCreditsDisplayName)]
        public int NumberOfCredits { get; set; }

        [Display(Name = MessageGiftSendDisplayName)]
        public string Message { get; set; }
    }
}
