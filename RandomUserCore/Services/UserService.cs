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

        public async Task<ReadOnlyListResult<User>> GetUsersList(Pagination pagination = null, string search = null)
        {
            if (pagination == null) pagination = new Pagination { Limit = 20, Skip = 0 };
            var userList = await _userRepository.GetUserListBySearchValue(pagination, search);
            var users = _mapper.Map<List<User>>(userList.ToList());
            return new ReadOnlyListResult<User>(users, users.Count, pagination.Skip, pagination.Limit);
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                var userEntity = await _userRepository.Create(_mapper.Map<UserEntity>(user));
                return _mapper.Map<User>(userEntity);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create the user");
                return null;
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                var userEntity = _mapper.Map<UserEntity>(user);
              var updatedUser =  await _userRepository.Update(userEntity);
                return _mapper.Map<User>(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update the user");
                return null;
            }
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            try
            {
                return await _userRepository.Delete(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Delete the user");
                return false;
            }
        }

        public async Task<bool> CreateRandomUsers()
        {
            try
            {
                var userEntityList = await _externalApiService.GetRandomUsers();
                return await _userRepository.BulkInsert(userEntityList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to bulk insert user");
                return false;
            }
        }

        public async Task<User> GetUserById(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                if (user == null) return null;
                return _mapper.Map<User>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No User Available");
                return null;
            }
        }
    }
}