﻿using Domains.Messaging.GroupEntity.Models;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Models;

namespace Domains.Messaging.GroupEntity.Repo;
public interface IUpdateGroupRepo {
    Task<Result> UpdateInfoAsync(GroupInfoUpdateModel updateModel , GroupTbl mainModel);
    Task<Result> UpdatePicturesAsync(LinkedList<Logo> pictures , GroupTbl mainModel);
    Task<Result> UpdateMembersAsync(LinkedList<GroupMemberTbl> members , GroupTbl mainModel);
}