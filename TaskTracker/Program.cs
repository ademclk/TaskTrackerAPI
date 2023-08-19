using TaskTracker.Repositories;
using TaskTracker.Repositories.Interfaces;
using TaskTracker.Services;
using TaskTracker.Services.Interfaces;
using TaskTracker.Utilities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));

builder.Services.AddSingleton<DataContext>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await context.Init();
}

{
    app.UseCors(c => c
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

    app.MapControllers();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
