using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.DAL;

public class ApiDbContext : IdentityDbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var groupUsers = modelBuilder.Entity<GroupMemberEntity>();

        groupUsers.HasKey(gu => new { gu.UserId, gu.GroupId });
        groupUsers.HasOne(gu => gu.User).WithMany(u => u.MemberingGroups);
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

    public DbSet<TaskEntity> ApiTasks { get; set; }
    public DbSet<TaskBoxEntity> ApiTaskBoxes { get; set; }
    public DbSet<UserGroupEntity> ApiGroups { get; set; }
    public DbSet<UserEntity> ApiUsers { get; set; }
    public DbSet<GroupMemberEntity> ApiGroupsUsers { get; set; }
}
