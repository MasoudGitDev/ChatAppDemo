﻿using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupMemberEntity.Repos;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class UnblockMemberGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupAdminManager(groupAdminRepo), IRequestHandler<UnblockMemberModel , Result> {
    public async Task<Result> Handle(UnblockMemberModel request , CancellationToken cancellationToken) {
        return await TryToDoActionByAdminAsync(
           request.GroupId ,
           request.AdminId ,
           request.MemberId ,
           async (member , _) => await groupAdminRepo.Commands.UnblockMemberAsync(member));
    }
}