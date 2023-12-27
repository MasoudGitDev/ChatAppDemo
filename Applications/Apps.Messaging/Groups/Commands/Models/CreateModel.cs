using Domains.Messaging.GroupEntity.Repo;
using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Apps.Messaging.Groups.Commands.Models;
public record CreateModel(IGroupRepo groupRepo) : IRequest<Result>
{

    [Required]
    public string CreatorId { get; set; } = string.Empty;
    [Required]
    public string DisplayId { get; set; } = string.Empty;

    public string? Picture { get; set; }
    public string? Description { get; set; }
    // public string? Categories { get; set; }
}
