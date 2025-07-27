// Daniil Abkhalimov, studentId: A00177578

namespace Assessment__2.Model;

// Declaring class for validation result data
public class ValidationResult
{
    // Declaring property for validation success status
    public bool IsValid { get; init; }
    // Declaring property for validation error message
    public string? Error { get; init; }
    // Creating success validation result
    public static ValidationResult Success() => new() { IsValid = true };
    // Creating failure validation result with error message
    public static ValidationResult Failure(string error) => new() { IsValid = false, Error = error };
}