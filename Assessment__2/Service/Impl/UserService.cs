using Assessment__2.Model;
using Assessment__2.Repository;

namespace Assessment__2.Service.Impl;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public User? AuthenticateUser(string username, string password)
    {
        var user = _userRepository.GetByUsername(username);
        
        if (user != null && user.Password == password)
        {
            return user;
        }
        
        return null;
    }

    public bool RegisterUser(User user)
    {
        return _userRepository.AddUser(user);
    }

    public bool IsUsernameUnique(string username)
    {
        return !_userRepository.IsUsernameExists(username);
    }
} 