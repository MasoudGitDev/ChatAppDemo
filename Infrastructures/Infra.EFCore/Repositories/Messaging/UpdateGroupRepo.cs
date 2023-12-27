using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Models;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Infra.EFCore.Repositories.Messaging.Exceptions;
using Shared.Models;

namespace Infra.EFCore.Repositories.Messaging {
    internal sealed class UpdateGroupRepo(AppDbContext appDbContext) : IUpdateGroupRepo {
        public async Task<Result> UpdateAdminsAsync(LinkedList<Admin> admins , GroupTbl mainModel) {
            mainModel.Admins = admins;
           return await TryToUpdateAsync(mainModel , nameof(UpdateAdminsAsync));
        }

        public async Task<Result> UpdateInfoAsync(GroupInfoUpdateModel updateModel , GroupTbl mainModel) {
            if(!String.IsNullOrWhiteSpace(updateModel.Description)) {
                mainModel.Description = updateModel.Description;
            }            
            mainModel.DisplayId = updateModel.DisplayId;
          return  await TryToUpdateAsync(mainModel , nameof(UpdateInfoAsync));
        }

        public async Task<Result> UpdateMembersAsync(LinkedList<Member> members , GroupTbl mainModel) {
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
                return new Result(true , null);
            }
            catch (Exception ex) {
                return new Result(false , new List<ErrorModel>() { new ErrorModel(methodName ,"OperationFailed",ex.Message) });
            }
        }
    }
}
