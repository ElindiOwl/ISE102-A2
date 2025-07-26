using Assessment__2.Enum;

namespace Assessment__2.Service.Impl;

public class SystemMessageService : ISystemMessageService
{
    public string GetMessage(SystemError error) => error switch
    {
        SystemError.InvalidIntegerInput => "Input is not a valid number",
        _ => "Unknown system error"
    };
} 