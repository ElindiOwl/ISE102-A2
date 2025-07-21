using Assessment__2.Configuration;
using Assessment__2.Services;
using Assessment__2.Enum;
using Assessment__2.Models;

namespace Assessment__2.Validators;

public class UserValidator(ValidationConfig config, IValidationMessageService validationMessages)
{
    private readonly ValidationConfig _config = config;
    private readonly IValidationMessageService _validationMessages = validationMessages;

    public ValidationResult ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.UsernameRequired));
        return ValidationResult.Success();
    }

    public ValidationResult ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.EmailRequired));
        return ValidationResult.Success();
    }

    public ValidationResult ValidateAge(int age)
    {
        if (age < 1 || age > 120)
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.AgeInvalid));
        return ValidationResult.Success();
    }

    public ValidationResult ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.PhoneRequired));
        return ValidationResult.Success();
    }

    public ValidationResult ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.PasswordRequired));
        else if (password.Length < _config.MinPasswordLength)
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.PasswordTooShort));
        return ValidationResult.Success();
    }
} 