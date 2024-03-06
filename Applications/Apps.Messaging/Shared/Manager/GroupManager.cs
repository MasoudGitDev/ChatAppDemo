using Apps.Messaging.GroupAdmins.Commands.Strategies;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.UnitOfWorks;
using MediatR;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Manager;
/// <summary>
/// Manage Group operations
/// </summary>
internal abstract partial class GroupManager<T, R>(IGroupMessagingUOW _unitOfWork)
    : ABSAdminStrategy, IRequestHandler<T , R> where T : IRequest<R> where R : IResult {
    public abstract Task<R> Handle(T request , CancellationToken cancellationToken);
    protected async Task SaveChangesAsync() => await _unitOfWork.SaveChangesAsync();

    protected async Task<Result> UseStrategyAsync(
        GroupMemberTbl admin ,
        GroupMemberTbl targetMember ,
        ResultMessage successResultMessage ,        
        Func<Task> doFinally ,
        Action? changeOwnerWhenDeputyNeeded =null ,
        AdminLevel? levelToAssign = null) {
        var strategyResult =CheckConditions(admin , targetMember , levelToAssign);
        if(strategyResult is StrategyResult.NeedDeputy) {
            var deputy = await GetDeputyAdmin(admin.GroupId);
            strategyResult = NeedDeputy(admin , deputy , changeOwnerWhenDeputyNeeded);
        }
        if(strategyResult is StrategyResult.Succeeded) {
            await doFinally.Invoke();
            return new Result(ResultStatus.Success , successResultMessage);
        }
        return new Result(ResultStatus.Failed , new(successResultMessage.Code , "Some thing is wrong."));
    }
}


