using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserLogIn> UserLogIns { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new {rp.PermissionId, rp.RoleId});
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserRole>()
            .HasKey(us => new {us.UserId, us.RoleId});
        base.OnModelCreating(modelBuilder);
    }
    
    
}