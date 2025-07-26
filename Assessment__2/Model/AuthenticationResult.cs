using Assessment__2.Enum;

namespace Assessment__2.Model;

// Declaring class for authentication result data
public class AuthenticationResult
{
    // Declaring property for authentication success status
    public bool IsSuccess { get; set; }
    // Declaring property for authenticated user
    public User? User { get; set; }
    // Declaring property for login error type
    public LoginError? Error { get; set; }
    // Declaring property for remaining login attempts
    public int RemainingAttempts { get; set; }
    // Declaring property for result message
    public string? Message { get; set; }
} 