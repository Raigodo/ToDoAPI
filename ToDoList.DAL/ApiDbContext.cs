
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.DAL;

public class ApiDbContext : IdentityDbContext<UserEntity>
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var groupUsers = modelBuilder.Entity<GroupsUsersEntity>();

        groupUsers.HasKey(gu => new { gu.UserId, gu.GroupId });
        groupUsers.HasOne(gu => gu.User).WithMany(u => u.GroupMemberships);
        groupUsers.HasOne(gu => gu.Group).WithMany(g => g.MembersInGroup);


        modelBuilder.Entity<UserEntity>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<GroupEntity>()
            .Property(g => g.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<TaskBoxEntity>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<TaskEntity>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TaskEntity> ApiTasks { get; set; }
    public DbSet<TaskBoxEntity> ApiTaskBoxes { get; set; }
    public DbSet<GroupEntity> ApiGroups { get; set; }
    public DbSet<GroupsUsersEntity> ApiGroupsUsers { get; set; }
}
