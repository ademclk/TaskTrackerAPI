using Dapper;
using TaskTracker.Repositories.Interfaces;
using TaskTracker.Utilities;

namespace TaskTracker.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly DataContext _context;

    public TaskRepository(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<IEnumerable<Entities.Task>> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Tasks
        """;

        return await connection.QueryAsync<Entities.Task>(sql);
    }

    public async Task Create(Entities.Task task)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            INSERT INTO Tasks (Proje, Bolum)
            VALUES (@Proje, @Bolum)
        """;
        await connection.ExecuteAsync(sql, task);
    }
}
