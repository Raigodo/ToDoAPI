using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ToDoList.API.Domain.Entities;

public class GroupMemberEntity
{
    //described in db context
    public int UserId { get; set; }
    public int GroupId { get; set; }

    [JsonIgnore]
    public UserEntity User { get; set; }
    [JsonIgnore]
    public UserGroupEntity Group { get; set; }
}
