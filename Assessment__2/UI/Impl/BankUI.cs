namespace Assessment__2.UI.Impl;

public class BankUi(IMainMenuUi mainMenuUi) : IBankUi
{
    private readonly IMainMenuUi _mainMenuUi = mainMenuUi;
    
    public void RunBankSystem()
    {
        _mainMenuUi.RunMainMenu();
    }
}