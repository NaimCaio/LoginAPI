using LoginAPI.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.EF.Context
{
    public class BaseContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Function> functions { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserRole> usersRoles { get; set; }

        public DbSet<RoleFunction> rolesFunctions { get; set; }
        public DbSet<FunctionPermission> functionsPermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=authDb; Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>(entity=>
                entity.HasKey(e=> new { e.UserId, e.RoleId })
                );

            modelBuilder.Entity<RoleFunction>(entity =>
                entity.HasKey(e => new { e.RoleId, e.FunctionId })
                );

            modelBuilder.Entity<FunctionPermission>(entity =>
                entity.HasKey(e => new { e.FunctionId, e.PermissionId })
                );
        }
    }
}
