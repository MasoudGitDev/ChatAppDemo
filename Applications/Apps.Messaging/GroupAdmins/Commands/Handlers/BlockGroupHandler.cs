﻿using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Shared;
using Domains.Messaging.GroupMemberEntity.Repos;
using MediatR;
using Shared.Models;
namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class BlockGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupAdminManager(groupAdminRepo) , IRequestHandler<BlockModel , Result> {
    public async Task<Result> Handle(BlockModel request , CancellationToken cancellationToken) {        
        return await TryToDoActionByAdminAsync(
            request.GroupId ,
            request.AdminId ,
            request.MemberId , 
            async (member,_) => await groupAdminRepo.Commands.BlockAsync(member, request.AdminId ,
                 request.StartBlockAt , request.EndBlockAt , request.Reason));      
    }
}
