using Assessment__2.Enum;
using Assessment__2.Model;

namespace Assessment__2.Model;

public class AuthenticationResult
{
    public bool IsSuccess { get; set; }
    public User? User { get; set; }
    public LoginError? Error { get; set; }
    public int RemainingAttempts { get; set; }
    public string? Message { get; set; }
} 