using LoginAPI.Model;
using LoginAPI.Model.DTO;
using LoginAPI.Model.Entity;
using LoginAPI.Model.Helper;
using LoginAPI.Model.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginAPI.Application
{
    public class LoginApplication:ILoginApplication
    {
        private ILoginService _loginService;

        public JwtConfig _jwtConfig { get; set; }
        public JwtHelper _jwtHelper { get; set; }
        public LoginApplication(ILoginService loginService, JwtConfig jwtConfig)
        {
            _loginService = loginService;
            _jwtConfig = jwtConfig;
            _jwtHelper =  new JwtHelper(_jwtConfig);

            
        }
        public LoginUserDTO Autheticate(User loginUser)
        {
            var auth = _loginService.Authenticate(loginUser);
            if (!auth)
                throw new System.Exception("Invalid Login Credentials: ");

            var claims = new List<Claim>();
            claims.Add(new Claim("displayname", loginUser.Name));

            // Add roles as multiple claims
            if (loginUser.Roles!=null){
                foreach (var role in loginUser.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
                }
            }

            // create a new token with token helper and add our claim
            // from `Westwind.AspNetCore`  NuGet Package
            var token = _jwtHelper.GetJwtToken(
                loginUser.Name,
                _jwtConfig.SigningKey,
                _jwtConfig.Issuer,
                _jwtConfig.Audience,
                TimeSpan.FromMinutes(_jwtConfig.Timeout),
                claims.ToArray());

            

            return new LoginUserDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                TimeOut = token.ValidTo
            };
        }

        public object AddUser(NewUser user)
        {
            var baseUser = new User()
            {
                Name = user.Name,
                Password = user.Password,
            };
            var isValidPassword = _loginService.verifyPassword(baseUser);
            if(isValidPassword)
            {
                _loginService.createNewUser(baseUser);
            }
            return null;

        }
    }
}
