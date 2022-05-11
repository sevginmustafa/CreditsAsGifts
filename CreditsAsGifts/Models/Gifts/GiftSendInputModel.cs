using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static CreditsAsGifts.Common.ModelValidation;

namespace CreditsAsGifts.Models.Gifts
{
    public class GiftSendInputModel
    {
        [Display(Name = RecipientPhoneNumberDisplayName)]
        [Required(ErrorMessage = PhoneNumberRequiredMessage)]
        [DataType(DataType.PhoneNumber, ErrorMessage = PhoneNumberInvalidErrorMessage)]
        [RegularExpression(@"^(0[0-9]{9})$", ErrorMessage = PhoneNumberLengthErrorMessage)]
        public string PhoneNumber { get; set; }

        [Display(Name = NumberOfCreditsDisplayName)]
        public int NumberOfCredits { get; set; }

        [Display(Name = CommentGiftSendDisplayName)]
        public string Comment { get; set; }
    }
}
