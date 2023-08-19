namespace TaskTracker.Utilities;
using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

public class DataContext
{
    private readonly DbSettings _dbSettings;

    public DataContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = $"Server={_dbSettings.Server}; Database={_dbSettings.Database}; Uid={_dbSettings.UserId}; Pwd={_dbSettings.Password};";
        return new MySqlConnection(connectionString);
    }

    public async Task Init()
    {
        await _initDatabase();
        await _initTables();
    }

    private async Task _initDatabase()
    {
        // Veri tabanı yoksa oluştur.
        var connectionString = $"Server={_dbSettings.Server}; Uid={_dbSettings.UserId}; Pwd={_dbSettings.Password};";
        using var connection = new MySqlConnection(connectionString);
        var sql = $"CREATE DATABASE IF NOT EXISTS `{_dbSettings.Database}`;";
        await connection.ExecuteAsync(sql);
    }

    private async Task _initTables()
    {
        // Tablolar yoksa oluştur.
        using var connection = CreateConnection();
        await _initTasks();

        async Task _initTasks()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS Tasks (
                    Id INT NOT NULL AUTO_INCREMENT,
                    Proje VARCHAR(255),
                    Bolum VARCHAR(255),
                    PRIMARY KEY (Id)
                );
            """;
            await connection.ExecuteAsync(sql);
        }
    }
}
