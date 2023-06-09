using ToDoList.Domain.Dto;
using ToDoList.Domain.Entities;

namespace ToDoList.DAL.Interfaces;

public interface ITaskBoxRepository
{
    Task<IEnumerable<TaskBoxEntity>> GetAllTaskBoxesAsync();
    Task<IEnumerable<TaskBoxEntity>> GetGroupRootTaskBoxesAsync(GroupEntity group);
    Task<TaskBoxEntity?> GetTaskBoxByIdAsync(int boxId);
    Task<TaskBoxEntity> CreateTaskBoxAsync(TaskBoxDto taskBoxDto);
    Task<bool> UpdateTaskBoxAsync(TaskBoxEntity taskBox,TaskBoxDto taskBoxDto);
    Task<bool> DeleteTaskBoxAsync(TaskBoxEntity taskBox);
}
