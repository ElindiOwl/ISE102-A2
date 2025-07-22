using Assessment__2.UI;

namespace Assessment__2;

public class Bank(IBankUi bankUi)
{
    private readonly IBankUi _bankUi = bankUi;
    
    public void Run()
    {
        _bankUi.RunBankSystem();
    }
} 