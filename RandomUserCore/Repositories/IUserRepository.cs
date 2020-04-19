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
        Task<UserEntity> Update(UserEntity entity);
        Task<IEnumerable<UserEntity>> GetUserListBySearchValue(Pagination pagination, string search = null);
        Task<bool> Delete(Guid userId);
        Task<bool> BulkInsert(IEnumerable<UserEntity> userEntityList);
        Task<UserEntity> GetUserById(Guid userId);
    }
}