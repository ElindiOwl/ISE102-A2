namespace Assessment__2.UI.Impl;

// Declaring class implementing bank UI with constructor injection
public class BankUi(IMainMenuUi mainMenuUi) : IBankUi
{
    // Declaring private field for main menu UI dependency
    private readonly IMainMenuUi _mainMenuUi = mainMenuUi;
    
    // Implementing method for running bank system
    public void RunBankSystem()
    {
        // Running main menu UI
        _mainMenuUi.RunMainMenu();
    }
}