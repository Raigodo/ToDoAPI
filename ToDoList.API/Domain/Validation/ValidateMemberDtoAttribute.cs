using System.ComponentModel.DataAnnotations;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Roles;

namespace ToDoList.API.Domain.Validation;

public class ValidateMemberDtoAttribute : ValidationAttribute
{
    public string[] _allowedValues { get; }

    public ValidateMemberDtoAttribute()
    {
        _allowedValues = new string[] {
            GroupMemberRoles.Member, 
            GroupMemberRoles.Admin 
        };
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var dto = (GroupMemberDto) value;
        if (_allowedValues.Contains(dto.Role))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}
