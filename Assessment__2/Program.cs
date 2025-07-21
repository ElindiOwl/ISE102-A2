using Assessment__2.Builder;

namespace Assessment__2;

internal static class Program
{
    private static void Main()
    {
        var bank = BankBuilder.CreateDefault();
        bank.Run();
    }
}