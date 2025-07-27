// Daniil Abkhalimov, studentId: A00177578

namespace Assessment__2.Utility;

// Declaring class for menu helper with constructor injection
public class MenuHelper(InputHelper inputHelper)
{
    // Declaring private field for logic
    private readonly InputHelper _inputHelper = inputHelper;

    // Implementing method to run any menu with custom title and options
    public void RunMenu(
        string menuTitle, 
        Dictionary<string, (string Description, Action Action)> menuOptions, 
        Func<bool> shouldExit
        )
    {
        
        while (!shouldExit())
        {
            try
            {
                // Display menu header
                Console.WriteLine($"\n=== {menuTitle} ===");
                
                // Display menu options
                foreach (var item in menuOptions)
                {
                    Console.WriteLine($"{item.Key}. {item.Value.Description}");
                }
                
                // Get user choice
                string choice = _inputHelper.GetInput("Enter your choice: ");
                
                // Validate menu choice
                if (!menuOptions.TryGetValue(choice, out var menuItem))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }
                
                // Execute selected menu action
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