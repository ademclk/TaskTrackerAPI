using System.ComponentModel.DataAnnotations;

namespace TaskTracker.ViewModels.Task;

public class CreateRequest
{
    [Required]
    public string? Proje { get; set; }

    [Required]
    public string? Bolum { get; set; }
}
