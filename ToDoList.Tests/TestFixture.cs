using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Entities;
using Moq;
using ToDoList.API.Domain.Roles;

namespace ToDoList.Tests;

public class TestFixture : IDisposable
{
    private readonly DbContextOptions<ApiDbContext> _options;
    public ApiDbContext DbContext { get; private set; }
    public Mock<UserManager<UserEntity>> UserManager { get; private set; }
    public RoleManager<IdentityRole> RoleManager { get; private set; }

    public TestFixture()
    {
        _options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        DbContext = new ApiDbContext(_options);
        DbContext.Database.EnsureCreated();


        UserManager = CreateMockUserManager(new List<UserEntity>
        {
            new UserEntity
            {
                Nickname = "Executioner",
                UserName = "Admin",
                Email = "admin@example.com",
            },
            new UserEntity
            {
                Nickname = "User1",
                UserName = "User1",
                Email = "user1@example.com",
            },
            new UserEntity
            {
                Nickname = "User2",
                UserName = "User2",
                Email = "user2@example.com",
            }
        });

        RoleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(DbContext),
            new IRoleValidator<IdentityRole>[0],
            new UpperInvariantLookupNormalizer(),
            null,
            null
        );
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }

    private Mock<UserManager<TUser>> CreateMockUserManager<TUser>(List<TUser> ls) where TUser : class
    {
        var store = new Mock<IUserStore<TUser>>();
        var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        mgr.Object.UserValidators.Add(new UserValidator<TUser>());
        mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
        mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
        mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

        return mgr;
    }
}
