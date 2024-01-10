using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Apps.Messaging.GroupRequests.Commands.Models;
public record RemoveGroupRequestModel : IRequest<Result> {
    [Required]
    public Guid GroupId { get; set; }
    [Required]
    public Guid RequesterId { get; set; }
}
