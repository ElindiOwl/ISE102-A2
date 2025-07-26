using Assessment__2.Builder;

namespace Assessment__2;

// Declaring internal static class for program entry point
internal static class Program
{
    // Declaring private static method for main entry point
    private static void Main()
    {
        // Creating bank instance using builder
        var bank = BankBuilder.CreateDefault();
        // Running bank application
        bank.Run();
    }
}