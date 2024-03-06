namespace Domains.Messaging.GroupMessageEntity;
public interface IMessageModel {
    public Guid GroupId { get; set; }
    public Guid MemberId { get; set; }
    public string Message { get; set; }
    public string? FileUrl { get; set; }
}
