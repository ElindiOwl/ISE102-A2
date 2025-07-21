using Assessment__2.Enum;

namespace Assessment__2.Services;

public interface ILoginMessageService
{
    string GetMessage(LoginError error);
} 