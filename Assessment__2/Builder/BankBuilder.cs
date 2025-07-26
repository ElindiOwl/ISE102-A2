using Assessment__2.Configuration;
using Assessment__2.Repository.Impl;
using Assessment__2.Service.Impl;
using Assessment__2.UI.Impl;
using Assessment__2.Utility;
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
        var systemMessages = new SystemMessageService();
        var inputHelper = new InputHelper(systemMessages);
        var userValidator = new UserValidator(validationConfig, validationMessages, userService);
        var userMenuUi = new UserMenuUi(inputHelper);
        var mainMenuUi = new MainMenuUi(authService, userService, validationMessages, userValidator, userMenuUi, inputHelper);
        var bankUi = new BankUi(mainMenuUi);
        return new Bank(bankUi);
    }
} 