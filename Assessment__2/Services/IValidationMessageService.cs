using Assessment__2.Enum;

namespace Assessment__2.Services;

public interface IValidationMessageService
{
    string GetMessage(ValidationError error);
}