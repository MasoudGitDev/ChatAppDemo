namespace Shared.DTOs.Group;
public record GroupResultDto(Guid groupId, string DisplayId, string Title, string? LogoURLs, bool IsRequestable, string? Description);