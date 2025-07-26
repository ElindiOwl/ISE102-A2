namespace Assessment__2.Configuration;
 
// Declaring class for validation configuration settings
public class ValidationConfig
{
    // Declaring property for minimum password length with default value
    public int MinPasswordLength { get; set; } = 5;
    // Declaring property for minimum username length with default value
    public int MinUsernameLength { get; set; } = 3;
} 