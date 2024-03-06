using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Models;
namespace Apps.Messaging.GroupAdmins.Commands.Models;
public record AddLogoModel : IRequest<Result> {
    public Guid GroupId { get; set; }
    public Guid AdminId { get; set; }
    public IFormFile Logo { get; set; }

  
}
