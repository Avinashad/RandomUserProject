using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RandomUserCore.Models;
using RandomUserCore.Models.coreModels;

namespace RandomUserCore.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> Create(UserEntity entity);
        Task Update(UserEntity entity);
        Task<IEnumerable<UserEntity>> GetUserListBySearchValue(Pagination pagination, string search = null);
        Task Delete(Guid userId);
        Task BulkInsert(IEnumerable<UserEntity> userEntityList);
    }
}