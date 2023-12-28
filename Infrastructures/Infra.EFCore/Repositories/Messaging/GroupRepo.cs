using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Models;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Infra.EFCore.Repositories.Messaging.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Extensions;
using Shared.Models;
using Shared.ValueObjects;

namespace Infra.EFCore.Repositories.Messaging {
    internal class GroupRepo(AppDbContext appDbContext) : IGroupRepo {
        public async Task<Result> CreateAsync(GroupTbl entity) {
            await appDbContext.GroupTbl.AddAsync(entity);
            await appDbContext.SaveChangesAsync();
            return new Result(ResultStatus.Success , null);
        }

        public async Task<Result> DeleteAsync(EntityId entityId) {
            var entity = await appDbContext.GroupTbl.FindAsync(entityId);
            if(entity == null) {
                throw new GroupRepoException("DeleteAsync" , "NullObj" , "Because of invalid <groupTbl.Id> , this entity is null.");
            }
            appDbContext.GroupTbl.Remove(entity);
            await appDbContext.SaveChangesAsync();
            return new Result(ResultStatus.Success , null);
        }

        public async Task<Result<GroupTbl>> GetAsync(EntityId entityId) {
            var findEntity = await appDbContext.GroupTbl.FindAsync(entityId);
            if(findEntity == null) {
                throw new GroupRepoException(nameof(GetAsync) , "NullObj" , "Because of invalid <groupTbl.GroupId> , this entity is null.");
            }
            return new Result<GroupTbl>(ResultStatus.Success , null, findEntity);
        }

        public async Task<Result> UpdateAsync(GroupTbl entity) {
            var findEntity = await appDbContext.GroupTbl.FindAsync(entity.GroupId);
            if(findEntity == null) {
                throw new GroupRepoException(nameof(UpdateAsync) , "NullObj" , "Because of invalid <groupTbl.GroupId> , this entity is null.");
            }
            var config = new TypeAdapterConfig();

            config.NewConfig<GroupTbl , GroupTbl>()
                .Ignore(p =>p.GroupId)
                .Ignore(p=>p.CreatedAt)
                .Ignore(x=>x.CreatorId)
                .Ignore(x=>x.Timestamp);


            appDbContext.GroupTbl.Update(entity.Adapt(findEntity,config));
            await appDbContext.SaveChangesAsync();
            return new Result(ResultStatus.Success , null);
        }
        public async Task<Result> UpdateInfoAsync(GroupTbl mainModel , GroupInfoUpdateModel updateModel) {
            appDbContext.GroupTbl.Update(updateModel.Adapt(mainModel));
            await appDbContext.SaveChangesAsync();
            return new Result(ResultStatus.Success , null);
        }

        public async Task<Result> CreateAdmins(
           EntityId GroupId , LinkedList<EntityId> memberIds , AdminAccessLevels accessLevel , EntityId adminModifierId
        ) {
            try {
                var unknownMembers = new List<EntityId>();
                foreach ( var memberId in memberIds ) {
                    var findMember = await appDbContext.AppUserGroupTbl
                        .Where(x=>x.GroupId ==  GroupId)
                        .Where(x=>x.MemberId == memberId)
                        .FirstOrDefaultAsync();
                    if(findMember is null) {
                        unknownMembers.Add(memberId);
                    }
                    else {
                        findMember.IsAdmin = true;
                        findMember.AdminInfo = new AdminInfo {
                            AccessLevel = accessLevel ,
                            AdminAt = DateTime.UtcNow ,
                            AdminModifierId = adminModifierId
                        };
                        appDbContext.AppUserGroupTbl.Update(findMember);
                    }
                }
                await appDbContext.SaveChangesAsync();
                
                return new Result(ResultStatus.Success , new ErrorModel("" , "NotCompletedAction" , $"This ids are invalid: [Numbers : {unknownMembers.Count}]" 
                    + Environment.NewLine + unknownMembers.ToJson()));
            }
            catch(Exception ex) {
                return new Result(ResultStatus.Failed , new ErrorModel("UpdateGroupRepo" , "CreateAdminsError" , ex.Message));
            }
        }
    }
}
