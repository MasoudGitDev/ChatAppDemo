using Domains.Messaging.Shared.ValueObjects;

namespace Client.WebAssembly.Models;
public record MessageModel {
    public AppUserId SenderId { get; init; }
    public string? Logo { get; set; }
    public string DisplayName { get; set; } 
    public string Message { get; set; }
}

