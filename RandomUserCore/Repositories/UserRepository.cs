using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RandomUserCore.Models;
using RandomUserCore.Models.coreModels;

namespace RandomUserCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RandomUserContext _context;

        public UserRepository(RandomUserContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> Create(UserEntity userEntity)
        {
            var createdUser = await _context.User.AddAsync(userEntity);
            await _context.SaveChangesAsync();
            return createdUser.Entity;
        }

        public async Task<UserEntity> Update(UserEntity entity)
        {
            var userEntity = await _context.User.Include(x => x.ImageDetail).FirstOrDefaultAsync(u => u.Id.Equals(entity.Id));
            if (userEntity == null) return null;

            userEntity.Title = entity.Title;
            userEntity.FirstName = entity.FirstName;
            userEntity.LastName = entity.LastName;
            userEntity.PhoneNumber = entity.PhoneNumber;
            userEntity.Email = entity.Email;
            userEntity.DateOfBirth = entity.DateOfBirth;
            userEntity.UpdatedAt = DateTime.Now;
            userEntity.ImageDetail.Original = entity.ImageDetail.Original;
            userEntity.ImageDetail.Thumbnail = entity.ImageDetail.Thumbnail;
            userEntity.ImageDetail.UpdatedAt = DateTime.Now;
            var updatedUser = _context.User.Update(userEntity);
            await _context.SaveChangesAsync();
            return updatedUser.Entity;


        }

        public async Task<bool> Delete(Guid userId)
        {
            var userEntity = await _context.User.Include(x => x.ImageDetail).FirstOrDefaultAsync(x => x.Id.Equals(userId));
            if (userEntity == null) return false;

            _context.User.Remove(userEntity);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;

        }
        public async Task<IEnumerable<UserEntity>> GetUserListBySearchValue(Pagination pagination, string searchValue)
        {
            if (searchValue != null)
            {
                return await _context.User.AsNoTracking().Include(u => u.ImageDetail).Where(x => x.FirstName.Contains(searchValue) || x.LastName.Contains(searchValue)).Skip(pagination.Skip).Take(pagination.Limit).ToListAsync();
            }
            else
            {
                return await _context.User.Include(u => u.ImageDetail).OrderBy(x => Guid.NewGuid()).Skip(pagination.Skip).Take(pagination.Limit).ToListAsync();

            }
        }

        public async Task<bool> BulkInsert(IEnumerable<UserEntity> userEntityList)
        {
            await _context.User.AddRangeAsync(userEntityList);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<UserEntity> GetUserById(Guid userId)
        {
            return await _context.User.AsNoTracking().Include(u => u.ImageDetail).FirstOrDefaultAsync(x => x.Id.Equals(userId));
        }
    }
}