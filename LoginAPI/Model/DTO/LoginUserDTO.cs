using System;

namespace LoginAPI.Model.DTO
{
    public class LoginUserDTO
    {
        public string Token { get; set; }
        public DateTime TimeOut { get; set; }
    }
}
