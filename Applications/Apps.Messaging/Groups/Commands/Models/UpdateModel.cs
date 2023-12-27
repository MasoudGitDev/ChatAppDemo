using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;
namespace Apps.Messaging.Groups.Commands.Models
{
    public record UpdateInfoModel:IRequest<Result>
    {
        [Required]
        public string GroupId { get; set; } = string.Empty;
        [Required]
        public string DisplayId { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
