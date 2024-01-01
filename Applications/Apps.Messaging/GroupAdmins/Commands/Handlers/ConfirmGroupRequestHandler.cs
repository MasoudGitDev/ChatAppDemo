﻿using Apps.Messaging.GroupAdmins.Commands.Models;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.Shared.Models;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class ConfirmGroupRequestHandler(IGroupAdminRepo groupAdminRepo)
    : IRequestHandler<ConfirmGroupRequestModel , Result> {
    public async Task<Result> Handle(ConfirmGroupRequestModel request , CancellationToken cancellationToken) {
        var adminMember = await groupAdminRepo.GetAdminAsync(request.GroupId,request.AdminId);
        if(adminMember == null) {
           return  new Result(ResultStatus.Failed , new("GetAdminAsync" , "NotAccess" , "You are not an admin."));
        }
        var groupRequest = await groupAdminRepo.GetRequestAsync(request.GroupId,request.RequesterId);
        if(groupRequest == null) {
           return new Result(ResultStatus.Failed , new("GetRequestAsync" , "NotFound" , "NotFound any request with that Id."));
        }
        var newMember = new GroupMemberTbl(){
            Id = Guid.NewGuid(),
            AdminInfo = null ,
            BlockMemberInfo = null ,
            IsAdmin = false ,
            IsBlocked = false ,
            GroupId = request.GroupId,
            MemberAt = DateTime.UtcNow,
            MemberId = request.RequesterId,
        };
        await groupAdminRepo.ConfirmedRequest(newMember , groupRequest);
        return new Result(ResultStatus.Success , null);
    }
}
