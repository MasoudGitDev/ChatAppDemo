using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;
namespace Apps.Messaging.GroupAdmins.Commands.Models;  
public record AddLogoModel :IRequest<Result> {
    [Required]
    public Guid GroupId { get; set; }
    [Required]
    public string Logo { get; set; } = String.Empty;
}
