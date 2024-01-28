using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Apps.Messaging.GroupRequests.Commands.Models;

public record UpdateRequestModel : IRequest<Result> {
    [Required]
    public Guid GroupId { get; set; }
    [Required]
    public Guid RequesterId { get; set; }
    [Required , Length(3 , 200)]
    public string Description { get; set; }
}
