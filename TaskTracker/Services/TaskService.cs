using AutoMapper;
using TaskTracker.Repositories.Interfaces;
using TaskTracker.Services.Interfaces;
using TaskTracker.ViewModels.Task;

namespace TaskTracker.Services;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<CreateRequest, Entities.Task>().ReverseMap();
    }
}

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public TaskService(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Entities.Task>> GetAllTasks()
    {
        return await _taskRepository.GetAll();
    }

    public async Task CreateTask(CreateRequest request)
    {
        Entities.Task task = _mapper.Map<Entities.Task>(request);

        await _taskRepository.Create(task);
    }
}
