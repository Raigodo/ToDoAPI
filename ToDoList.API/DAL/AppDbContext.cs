using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL.Interfaces;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var groupUsers = modelBuilder.Entity<GroupsUsersEntity>();

        groupUsers.HasKey(gu => new { gu.UserId, gu.GroupId });
        groupUsers.HasOne(gu => gu.User).WithMany(u => u.MemberInGroups);
        groupUsers.HasOne(gu => gu.Group).WithMany(g => g.MembersInGroup);


        modelBuilder.Entity<UserEntity>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<UserGroupEntity>()
            .Property(g => g.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<TaskBoxEntity>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<TaskEntity>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();
    }

    public DbSet<TaskEntity> ToDoTasks { get; set; }
    public DbSet<TaskBoxEntity> TaskBoxes { get; set; }
    public DbSet<UserGroupEntity> Groups { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<GroupsUsersEntity> GroupUsers { get; set; }
}
