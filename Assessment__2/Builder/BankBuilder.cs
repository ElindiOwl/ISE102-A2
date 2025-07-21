using Assessment__2.Configuration;
using Assessment__2.Repositories;
using Assessment__2.Services;
using Assessment__2.UI;

namespace Assessment__2.Builder;

public static class BankBuilder
{
    public static Bank CreateDefault()
    {
        var loginConfig = new LoginConfig();
        var validationConfig = new ValidationConfig();
        
        var userRepository = new UserRepository();
        var userService = new UserService(userRepository);
        var loginMessages = new LoginMessageService(loginConfig);
        var authService = new AuthenticationService(userService, loginConfig, loginMessages);
        var validationMessages = new ValidationMessageService(validationConfig);
        
        var bankUI = new BankUI(authService, userService, validationMessages, validationConfig);

        return new Bank(bankUI);
    }
} 