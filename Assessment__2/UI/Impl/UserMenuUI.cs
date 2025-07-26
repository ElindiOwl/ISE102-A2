using Assessment__2.Utility;

namespace Assessment__2.UI.Impl;

// Declaring class implementing user menu UI with constructor injection
public class UserMenuUi(InputHelper inputHelper) : IUserMenuUi
{
    // Declaring private field for input helper dependency
    private readonly InputHelper _inputHelper = inputHelper;
    
    // Implementing method for running user menu
    public void RunUserMenu()
    {
        // Declaring flag for menu exit control
        bool exitUserMenu = false;
        // Creating user menu options dictionary (yet they do nothing)
        var userMenu = new Dictionary<string, (string Description, Action Action)>
        {
            { "1", ("View Balance", () => { }) },
            { "2", ("Deposit", () => { }) },
            { "3", ("Withdraw", () => { }) },
            { "4", ("Transfer", () => { }) },
            { "0", ("Quit", () => exitUserMenu = true) }
        };
        
        // Running menu loop until exit
        while (!exitUserMenu)
        {
            try
            {
                // Displaying user menu header
                Console.WriteLine("\n=== User Menu ===");
                
                // Displaying menu options
                foreach (var item in userMenu)
                {
                    Console.WriteLine($"{item.Key}. {item.Value.Description}");
                }
                
                // Getting user choice
                string choice = _inputHelper.GetInput("Enter your choice: ");
                
                // Validating menu choice
                if (!userMenu.TryGetValue(choice, out var menuItem))
                {
                    // Displaying invalid choice message
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }
                
                // Executing selected menu action
                menuItem.Action();
            }
            catch (Exception exception)
            {
                // Displaying unexpected error message for user
                Console.WriteLine("An unexpected error occurred. Please try again.");
                // Logging exception details for developer
                Console.Error.WriteLine(exception);
            }
        }
    }
}