namespace CreditsAsGifts.Common
{
    public static class ModelValidation
    {
        public const string FirstNameDisplayName = "First name";
        public const string FirstNameRequiredMessage = "First name is required!";
        public const string FirstNameLengthErrorMessage = "The {0} must be at least {2} and at max {1} characters long!";
        public const int FirstNameMinLength = 2;
        public const int FirstNameMaxLength = 100;

        public const string LastNameDisplayName = "Last name";
        public const string LastNameRequiredMessage = "Last name is required!";
        public const string LastNameLengthErrorMessage = "The {0} must be at least {2} and at max {1} characters long!";
        public const int LastNameMinLength = 2;
        public const int LastNameMaxLength = 100;
        
        public const string UsernameDisplayName = "Username";
        public const string UsernameRequiredMessage = "Username is required!";
        public const string UsernameLengthErrorMessage = "The {0} must be at least {2} and at max {1} characters long!";
        public const int UsernameMinLength = 3;
        public const int UsernameMaxLength = 20;

        public const string RememberMeDisplayName = "Remember me?";

        public const string EmailRequiredMessage = "Email address is required!";
        public const string EmailInvalidErrorMessage = "Invalid email address!"; 
        
        public const string PhoneNumberDisplayName = "Phone number";
        public const string PhoneNumberRequiredMessage = "Phone number is required!";
        public const string PhoneNumberInvalidErrorMessage = "Invalid phone number!";
        public const string PhoneNumberLengthErrorMessage = "Invalid phone number! The phone number should start with 0 and should be 10 digits long!";

        public const string PasswordRequiredMessage = "Password is required!";
        public const string PasswordLengthErrorMessage = "The {0} must be at least {2} and at max {1} characters long!";
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;

        public const string ConfirmPasswordDisplayName = "Confirm password";
        public const string PasswordCompareErrorMessage = "The password and confirmation password do not match!";

        public const string NumberOfCreditsDisplayName = "Number of credits you want to send";
        public const string MessageGiftSendDisplayName = "Send a message or write a wish";
        public const string RecipientPhoneNumberDisplayName = "Phone number of the recipient";
    }
}
