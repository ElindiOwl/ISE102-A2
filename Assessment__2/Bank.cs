using Assessment__2.UI;

namespace Assessment__2;

public class Bank(IBankUI bankUI)
{
    private readonly IBankUI _bankUI = bankUI;
    
    public void Run()
    {
        _bankUI.RunBankSystem();
    }
} 