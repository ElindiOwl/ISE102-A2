using Assessment__2.Enum;

namespace Assessment__2.Service;

public interface IValidationMessageService
{
    string GetMessage(ValidationError error);
}