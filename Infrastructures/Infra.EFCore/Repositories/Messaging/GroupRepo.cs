using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Models;
using Domains.Messaging.GroupEntity.Repo;
using Infra.EFCore.Contexts;
using Infra.EFCore.Repositories.Messaging.Exceptions;
using Mapster;
using Shared.Models;

namespace Infra.EFCore.Repositories.Messaging {
    internal class GroupRepo(AppDbContext appDbContext) : IGroupRepo {
        public async Task<Result> CreateAsync(GroupTbl entity) {
            await appDbContext.GroupTbl.AddAsync(entity);
            await appDbContext.SaveChangesAsync();
            return new Result(true , null);
        }

        public async Task<Result> DeleteAsync(string entityId) {
            var entity = await appDbContext.GroupTbl.FindAsync(entityId);
            if(entity == null) {
                throw new GroupRepoException("DeleteAsync" , "NullObj" , "Because of invalid <groupTbl.Id> , this entity is null.");
            }
            appDbContext.GroupTbl.Remove(entity);
            await appDbContext.SaveChangesAsync();
            return new Result(true , null);
        }

        public async Task<Result<GroupTbl>> GetAsync(string entityId) {
            var findEntity = await appDbContext.GroupTbl.FindAsync(entityId);
            if(findEntity == null) {
                throw new GroupRepoException(nameof(GetAsync) , "NullObj" , "Because of invalid <groupTbl.GroupId> , this entity is null.");
            }
            return new Result<GroupTbl>(true , null, findEntity);
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
            return new Result(true , null);
        }
        public async Task<Result> UpdateInfoAsync(GroupTbl mainModel , GroupInfoUpdateModel updateModel) {
            appDbContext.GroupTbl.Update(updateModel.Adapt(mainModel));
            await appDbContext.SaveChangesAsync();
            return new Result(true , null);
        }
    }
}
