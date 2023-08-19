using TaskTracker.ViewModels.Task;

namespace TaskTracker.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<Entities.Task>> GetAllTasks();
    Task CreateTask(CreateRequest request);
}
