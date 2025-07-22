using Assessment__2.Utility;

namespace Assessment__2.UI;

public class UserMenuUi : IUserMenuUi
{
    public void RunUserMenu()
    {
        bool exitUserMenu = false;
        var userMenu = new Dictionary<string, (string Description, Action Action)>
        {
            { "1", ("View Balance", () => { }) },
            { "2", ("Deposit", () => { }) },
            { "3", ("Withdraw", () => { }) },
            { "4", ("Transfer", () => { }) },
            { "0", ("Quit", () => exitUserMenu = true) }
        };
        
        while (!exitUserMenu)
        {
            try
            {
                Console.WriteLine("\n=== User Menu ===");
                
                foreach (var item in userMenu)
                {
                    Console.WriteLine($"{item.Key}. {item.Value.Description}");
                }
                
                string choice = InputHelper.GetInput("Enter your choice: ");
                
                if (!userMenu.TryGetValue(choice, out var menuItem))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }
                
                menuItem.Action();
            }
            catch (Exception exception)
            {
                Console.WriteLine("An unexpected error occurred. Please try again.");
                Console.Error.WriteLine(exception);
            }
        }
    }
} 