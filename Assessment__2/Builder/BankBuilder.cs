using Assessment__2.Configuration;
using Assessment__2.Repository.Impl;
using Assessment__2.Service.Impl;
using Assessment__2.UI.Impl;
using Assessment__2.Utility;
using Assessment__2.Validator;
using Assessment__2.Controller;

namespace Assessment__2.Builder;

// Declaring static class for building bank instances
public static class BankBuilder
{
    // Creating default bank configuration
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
        var loginValidator = new LoginValidator(loginMessages);
        var userMenuUi = new UserMenuUi(inputHelper);
        var loginController = new LoginController(authService, userMenuUi, inputHelper, loginValidator);
        var signUpController = new SignUpController(userService, validationMessages, userValidator, userMenuUi, inputHelper);
        var mainMenuUi = new MainMenuUi(validationMessages, inputHelper, loginController, signUpController);
        var bankUi = new BankUi(mainMenuUi);
        return new Bank(bankUi);
    }
} 