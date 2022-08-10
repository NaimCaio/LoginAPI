using System.Collections.Generic;

namespace LoginAPI.Model
{
    public class NewUser
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public List<string> Roles { get; set; }

    }
}
