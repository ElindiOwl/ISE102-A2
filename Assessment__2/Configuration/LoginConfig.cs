namespace Assessment__2.Configuration;

public class LoginConfig
{
    public int MaxLoginAttempts { get; set; } = 3;
    public int LockoutDurationMinutes { get; set; } = 1;
} 