namespace Assessment__2.Models;

public class ValidationResult
{
    public bool IsValid { get; init; }
    public string? Error { get; init; }
    public static ValidationResult Success() => new() { IsValid = true };
    public static ValidationResult Failure(string error) => new() { IsValid = false, Error = error };
}