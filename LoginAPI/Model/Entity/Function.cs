using System.Collections.Generic;

namespace LoginAPI.Model.Entity
{
    public class Function
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<FunctionPermission> Permissions { get; set; }
    }
}
