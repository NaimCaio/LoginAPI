using LoginAPI.Model.Entity;

namespace LoginAPI.Model.Interfaces
{
    public interface ILoginService
    {
        bool Authenticate(User user);
        bool verifyPassword(User user);
        User createNewUser(User user);
    }
}
