using LoginAPI.Model;
using LoginAPI.Model.Entity;
using LoginAPI.Model.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace LoginAPI.Services
{
    public class LoginService: ILoginService
    {
        private readonly IBaseRepository<User> _usersRepository;
        private readonly ILogger<LoginService> _log;
        private readonly Password passwordFunctions;
        public LoginService(IBaseRepository<User> usersRepository, ILogger<LoginService> log)
        {
            _usersRepository = usersRepository;
            _log = log;
            passwordFunctions=new Password();
        }
        public bool Authenticate( User  user)
        {
            var baseUser = _usersRepository.Where(u=>  u.Name==user.Name).ToList().FirstOrDefault();
            var isAuthenticated = false;
            if(baseUser != null)
            {
                isAuthenticated = passwordFunctions.VerifyHashedPassword(user.Password, baseUser.Password)== PasswordVerificationResult.Success;
            }
            return isAuthenticated;
        }

        public User createNewUser(User user)
        {
            var isNewUser = _usersRepository.FirstOrDeafault(u=> u.Name==user.Name)==null;
            if (isNewUser)
            {
                var hashPassword = passwordFunctions.HashPassword(user.Password);
                user.Password = hashPassword;
                try
                {
                    _usersRepository.Add(user);
                    _usersRepository.Save();
                    return user;    
                }
                catch (System.Exception)
                {

                    throw new Exception("Erro ao adicionar");
                }

            }
            else
            {
                throw new Exception("Usuário ja existe");
            }
        }

        public bool verifyPassword(User user)
        {
            return  passwordFunctions.PasswordValidation(user.Password)== PasswordVerificationResult.Success;
        }
    }
}
