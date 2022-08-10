using LoginAPI.Model;
using LoginAPI.Model.DTO;
using LoginAPI.Model.Entity;
using LoginAPI.Model.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginApplication _loginApplication;

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, ILoginApplication loginApplication)
        {
            _logger = logger;
            _loginApplication = loginApplication;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Auth")]
        public LoginUserDTO Authenticate(string Name, string Password)
        {
            var user = new User()
            {Name = Name, Password = Password};
           return _loginApplication.Autheticate(user);
        }

        [HttpPost]
        [Route("CreateUser")]
        public object CreateUser(string Name,  string Password, List<string> Roles )
        {
            var user = new NewUser()
            {
                Name = Name,
                Password = Password,
                Roles = Roles

            };
            return _loginApplication.AddUser(user);
        }
    }
}
