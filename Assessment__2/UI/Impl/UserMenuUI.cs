// Daniil Abkhalimov, studentId: A00177578

using Assessment__2.Utility;

namespace Assessment__2.UI.Impl;

// Declaring class implementing user menu UI with constructor injection
public class UserMenuUi(MenuHelper menuHelper) : IUserMenuUi
{
    // Declaring private field for menu helper dependency
    private readonly MenuHelper _menuHelper = menuHelper;
    
    // Implementing method for running user menu
    public void RunUserMenu()
    {
        // Declaring local menu flag for menu exit control
        var exitUserMenu = false;
        
        // Creating user menu options dictionary
        var userMenuOptions = new Dictionary<string, (string Description, Action Action)>
        {
            { "1", ("View Balance", () => { }) },
            { "2", ("Deposit", () => { }) },
            { "3", ("Withdraw", () => { }) },
            { "4", ("Transfer", () => { }) },
            { "0", ("Quit", () => exitUserMenu = true) }
        };

        // Running user menu with exit condition
        _menuHelper.RunMenu("User Menu", userMenuOptions,() => exitUserMenu);
    }
}