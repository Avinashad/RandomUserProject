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
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(Guid userId);
        Task<bool> CreateRandomUsers();
        Task<User> GetUserById(Guid userId);
    }
}