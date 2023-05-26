using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.Domain.Dto
{
    public class GroupMemberDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
