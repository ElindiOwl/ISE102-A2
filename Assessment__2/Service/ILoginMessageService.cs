using Assessment__2.Enum;

namespace Assessment__2.Service;
 
public interface ILoginMessageService
{
    string GetMessage(LoginError error);
} 