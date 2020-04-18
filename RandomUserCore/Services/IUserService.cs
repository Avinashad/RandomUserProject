using System;
using System.Threading.Tasks;
using RandomUserCore.Models;
using RandomUserCore.Models.coreModels;
using RandomUserCore.Models.IntegrationModels;

namespace RandomUserCore.Services
{
    public interface IUserService
    {
        Task<ReadOnlyListResult<User>> GetUsersList(Pagination pagination, string search = null);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(Guid userId);
        Task CreateRandomUser();
    }
}