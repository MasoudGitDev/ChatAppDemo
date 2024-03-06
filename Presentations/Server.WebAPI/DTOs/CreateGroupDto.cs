using System.ComponentModel.DataAnnotations;

namespace Server.WebAPI.DTOs;

public class CreateGroupDto {
    [Required]
    public string DisplayId { get; set; } = string.Empty;
    [Required]
    public string Title { get; set; } = String.Empty;

    public string? Logo { get; set; }
    public string? Description { get; set; }
    public string? Categories { get; set; } = "Others";
    public bool IsRequestable { get; set; } = false;
}
