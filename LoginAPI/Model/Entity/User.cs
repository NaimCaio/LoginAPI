using System.Collections.Generic;

namespace LoginAPI.Model.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public List<UserRole> Roles {get; set; }
    }
}
