using Shared.Extensions;

namespace Domains.Messaging.Shared.ValueObjects;  
public record Member {
    public string UserId { get; init; } = String.Empty;
    public string Pictures { get; init; } = String.Empty;
    public DateTime MemberAt { get; init; }
    public bool IsOnline { get; set; } = false;

    public Member()
    {
        
    }
    public Member(string userId , string pictures , DateTime memberAt) {
        UserId = userId;
        Pictures = pictures;
        MemberAt = memberAt;
    }

    public static implicit operator string(Member member) 
        => member.ToJson();
    public static implicit operator Member(string jsonSource) 
        => jsonSource.FromJsonTo<Member>() ?? new Member();

}
