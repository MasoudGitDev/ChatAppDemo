using Domains.Messaging.Shared.ValueObjects;

namespace Domains.Messaging.GroupEntity.Models;
public record GroupModel
{
    public DisplayId DisplayId { get; set; }
    public AppUserId CreatorId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsRequestable { get; set; }
    public LinkedList<string> Categories { get; set; }
    public LinkedList<Logo> LogoURLs { get; set; }
}
