using Microsoft.AspNetCore.Identity;
using Moq;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Entities;
using ToDoList.API.Domain.Roles;

namespace ToDoList.Tests;

public class UserTests : IClassFixture<TestFixture>
{
    private readonly UserManager<UserEntity> _userManager;

    public UserTests(TestFixture fixture)
    {
        _userManager = fixture.UserManager.Object;
    }

    [Fact]
    public void TestUserLogin()
    {

    }
}
