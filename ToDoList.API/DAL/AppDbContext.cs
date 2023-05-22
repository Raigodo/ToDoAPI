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
        var box = modelBuilder.Entity<TaskBoxEntity>();
        var group = modelBuilder.Entity<GroupEntity>();
        var user = modelBuilder.Entity<UserEntity>();
        var groupUsers = modelBuilder.Entity<GroupsUsersEntity>();


        box.HasMany(b => b.SubFolders).WithOne(b => b.ParrentBox);
        box.HasMany(b => b.Tasks).WithOne(t => t.ParrentBox);
        box.HasOne(b => b.AssociatedGroup).WithMany(g => g.AcessibleBoxes);


        groupUsers.HasKey(gu => new { gu.UserId, gu.GroupId });
        groupUsers.HasOne(gu => gu.User).WithMany(u => u.MemberInGroups);
        groupUsers.HasOne(gu => gu.Group).WithMany(g => g.MembersInGroup);
    }

    public DbSet<TaskEntity> ToDoTasks { get; set; }
    public DbSet<TaskBoxEntity> TaskBoxes { get; set; }
    public DbSet<GroupEntity> Groups { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<GroupsUsersEntity> GroupUsers { get; set; }
}
