namespace LoginAPI.Model.Entity
{
    public class FunctionPermission
    {
        public int FunctionId { get; set; }
        public Function Function { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
