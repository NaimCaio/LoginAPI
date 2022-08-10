using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace LoginAPI.Model
{
    public class UserPasswordValidator : IIdentityValidator<string>
    {
        public Task<IdentityResult> ValidateAsync(string item)
        {
            throw new System.NotImplementedException();
        }
    }
}
