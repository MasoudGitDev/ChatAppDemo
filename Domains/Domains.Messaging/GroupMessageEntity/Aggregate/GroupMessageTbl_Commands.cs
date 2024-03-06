using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;

namespace Domains.Messaging.GroupMessageEntity.Aggregate;
public partial record class GroupMessageTbl {

    public static GroupMessageTbl Create(IMessageModel model) 
            => new GroupMessageTbl() {
                Id = Guid.NewGuid() ,
                GroupId = model.GroupId ,
                AppUserId = model.MemberId ,
                Message = model.Message ,
                FileUrl = model.FileUrl ,
                FirstChecked = false ,
                LastChecked = false ,
    };

    public void Update(string message , string? fileUrl) {
        this.Message = message;
        this.FileUrl = fileUrl;
    }
    public void changeFirstCheckTo(bool isChecked) {
        FirstChecked = isChecked;
    }
    public void changeLastCheckTo(bool isChecked) {
        LastChecked = isChecked;
    }

}
