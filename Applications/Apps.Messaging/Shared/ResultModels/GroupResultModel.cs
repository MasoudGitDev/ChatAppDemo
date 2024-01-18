using Domains.Messaging.GroupEntity.ValueObjects;

namespace Apps.Messaging.Shared.ResultModels;  
public record GroupResultModel(GroupId GroupId , bool IsRequestable , DateTime CreatedAt);
