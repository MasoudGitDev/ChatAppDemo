namespace Domains.Messaging.Shared.Models;  
public record MemberInfo(
    Guid MemberId ,
    DateTime MemberAt ,
    bool IsAdmin ,
    bool IsBlocked
);
