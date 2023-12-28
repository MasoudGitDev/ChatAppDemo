using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Models;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Infra.EFCore.Repositories.Messaging.Exceptions;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Models;
using Shared.ValueObjects;

namespace Infra.EFCore.Repositories.Messaging
{
    internal sealed class UpdateGroupRepo(AppDbContext appDbContext) : IUpdateGroupRepo {

        public async Task<Result> UpdateInfoAsync(GroupInfoUpdateModel updateModel , GroupTbl mainModel) {
            if(!String.IsNullOrWhiteSpace(updateModel.Description)) {
                mainModel.Description = updateModel.Description;
            }            
            mainModel.DisplayId = updateModel.DisplayId;
          return  await TryToUpdateAsync(mainModel , nameof(UpdateInfoAsync));
        }

        public async Task<Result> UpdateMembersAsync(LinkedList<GroupMemberTbl> members , GroupTbl mainModel) {
            mainModel.Members = members;
           return await TryToUpdateAsync(mainModel , nameof(UpdateMembersAsync));
        }

        public async Task<Result> UpdatePicturesAsync(LinkedList<Logo> pictures , GroupTbl mainModel) {
            mainModel.Logos = pictures;
           return await TryToUpdateAsync(mainModel , nameof(UpdatePicturesAsync));
        }

       

        

        private async Task<Result> TryToUpdateAsync(GroupTbl updatedModel , string methodName) {
            try {
                appDbContext.GroupTbl.Update(updatedModel);
                await appDbContext.SaveChangesAsync();
                return new Result(ResultStatus.Success , null);
            }
            catch (Exception ex) {
                return new Result(ResultStatus.Failed , new ErrorModel(methodName ,"OperationFailed",ex.Message));
            }
        }
    }
}
