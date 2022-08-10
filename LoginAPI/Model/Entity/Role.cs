using System.Collections.Generic;

namespace LoginAPI.Model.Entity
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoleFunction> Functions { get; set; }
    }
}
