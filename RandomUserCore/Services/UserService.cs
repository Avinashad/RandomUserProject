using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using RandomUserCore.ExternalApi;
using RandomUserCore.Models;
using RandomUserCore.Models.coreModels;
using RandomUserCore.Models.IntegrationModels;
using RandomUserCore.Repositories;

namespace RandomUserCore.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IExternalApiService _externalApiService;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IMapper mapper, IExternalApiService externalApiService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
            _externalApiService = externalApiService;
        }

        public async Task<ReadOnlyListResult<User>> GetUsersList(Pagination pagination, string search = null)
        {
            var userList = await _userRepository.GetUserListBySearchValue(pagination, search);
            var users = _mapper.Map<List<User>>(userList.ToList());
            return new ReadOnlyListResult<User>(users, users.Count, pagination.Skip, pagination.Limit);
        }

        public async Task CreateUser(User user)
        {
            try
            {
                //In case where you want to avoid multiple call to the DB
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var userEntity = _mapper.Map<UserEntity>(user);
                    var createdUser = await _userRepository.Create(userEntity);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create the user");
                throw;
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                var userEntity = _mapper.Map<UserEntity>(user);
                await _userRepository.Update(userEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update the user");
                throw;
            }
        }

        public async Task DeleteUser(Guid userId)
        {
            try
            {
                await _userRepository.Delete(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Delete the user");
                throw;
            }
        }

        public async Task CreateRandomUser()
        {
            try
            {
                var userEntityList = await _externalApiService.GetRandomUsers();
                await _userRepository.BulkInsert(userEntityList);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Failed to bulk insert user");
                throw;
            }
        }
    }
}