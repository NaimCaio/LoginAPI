using LoginAPI.Model.DTO;
using LoginAPI.Model.Entity;
using System.Threading.Tasks;

namespace LoginAPI.Model.Interfaces
{
    public interface ILoginApplication
    {
        LoginUserDTO Autheticate(User user);

        object AddUser(NewUser user);
    }
}
