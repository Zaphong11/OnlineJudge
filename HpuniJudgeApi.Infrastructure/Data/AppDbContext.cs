using HpuniJudgeApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ToDoAppApi.Domain;

namespace HpuniJudgeApi.Infrastructure.Data;
//DB Settings tableName
public class AppDbContext : DbContext
{ 
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //1. User table settings
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(x => x.Email).IsUnique();
        });

        //2. Role table settings
        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.ToTable("Roles");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).IsRequired().HasMaxLength(50);
            entity.HasIndex(x => x.Name).IsUnique();
            
            entity.HasData(
                new RoleEntity { Id = 1, Name = "Admin" },
                new RoleEntity { Id = 2, Name = "User" },
                new RoleEntity { Id = 3, Name = "Guest" }
                );
        });
        
        //3. UserRole table settings
        modelBuilder.Entity<UserRoleEntity>(entity =>
        {
            entity.ToTable("UserRoles");
            //composite key
            //Make sure that the primary key is unique
            entity.HasKey(x => new { x.UserId, x.RoleId });
            
            //Link to User table one-to-many relationship
            entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade); //User is deleted when Role is deleted
            
            //Link to Role table one-to-many relationship
            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade); //Role is deleted when User is deleted
        });
    }
}