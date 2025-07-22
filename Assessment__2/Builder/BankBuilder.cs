using Assessment__2.Configuration;
using Assessment__2.Repository.Impl;
using Assessment__2.Service.Impl;
using Assessment__2.UI.Impl;
using Assessment__2.Validator;

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
        var userValidator = new UserValidator(validationConfig, validationMessages);
        var userMenuUi = new UserMenuUi();
        var mainMenuUi = new MainMenuUi(authService, userService, validationMessages, userValidator, userMenuUi);
        var bankUi = new BankUi(mainMenuUi);
        return new Bank(bankUi);
    }
} 