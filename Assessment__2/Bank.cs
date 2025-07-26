using Assessment__2.UI;

namespace Assessment__2;

// Declaring class for bank application with constructor injection
public class Bank(IBankUi bankUi)
{
    // Declaring private field for bank UI dependency
    private readonly IBankUi _bankUi = bankUi;
    
    // Implementing method for running bank application
    public void Run()
    {
        // Running bank system UI
        _bankUi.RunBankSystem();
    }
} 