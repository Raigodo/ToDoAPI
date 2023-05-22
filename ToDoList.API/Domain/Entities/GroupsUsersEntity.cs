using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.API.Domain.Entities;

public class GroupsUsersEntity
{
    //described in db context
    public int UserId { get; set; }
    public int GroupId { get; set; }

    public UserEntity User { get; set; }
    public UserGroupEntity Group { get; set; }
}
