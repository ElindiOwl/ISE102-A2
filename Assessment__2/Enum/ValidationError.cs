namespace Assessment__2.Enum;

// Declaring enumeration for validation error types
public enum ValidationError
{
    UsernameRequired,
    UsernameExists,
    EmailRequired,
    AgeInvalid,
    PhoneRequired,
    PasswordRequired,
    PasswordTooShort,
    RegistrationFailed,
    UnexpectedError
}