namespace TaskTracker.Repositories.Interfaces;

/// Görevlerin listelenmesi, oluşturulması ve güncellenmesi için arayüz.
public interface ITaskRepository
{
    Task<IEnumerable<Entities.Task>> GetAll();
    Task Create(Entities.Task task);
}
