using Assessment__2.Enum;

namespace Assessment__2.Service;

public interface ISystemMessageService
{
    string GetMessage(SystemError error);
} 