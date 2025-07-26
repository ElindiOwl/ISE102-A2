namespace Assessment__2.Configuration;

// Declaring class for login configuration settings
public class LoginConfig
{
    // Declaring property for maximum login attempts with default value
    public int MaxLoginAttempts { get; set; } = 3;
    // Declaring property for lockout duration in minutes with default value
    public int LockoutDurationMinutes { get; set; } = 1;
} 