﻿using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupRequestEntity.Repos;

namespace Infra.EFCore.Repositories.Messaging.Group.Members;
internal class GroupAdminRepo(
    IGroupAdminCommands commands, IGroupAdminQueries queries, IGroupRepo general , IGroupRequestRepo groupRequestRepo) : IGroupAdminRepo
{
    public IGroupAdminCommands Commands => commands;

    public IGroupAdminQueries Queries => queries;

    public IGroupRepo General => general;

    public IGroupRequestRepo RequestRepo => groupRequestRepo;
}