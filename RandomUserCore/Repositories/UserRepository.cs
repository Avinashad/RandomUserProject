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

        public async Task Update(UserEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.User.Update(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(userId));
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<UserEntity>> GetUserListBySearchValue(Pagination pagination, string searchValue)
        {
            if (searchValue != null)
            {
                return await _context.User.Include(u => u.ImageDetail).Where(x => x.FirstName.Contains(searchValue) || x.LastName.Contains(searchValue)).Skip(pagination.Skip).Take(pagination.Limit).ToListAsync();
            }
            else
            {
                return await _context.User.Include(u => u.ImageDetail).OrderBy(x => Guid.NewGuid()).Skip(pagination.Skip).Take(pagination.Limit).ToListAsync();

            }
        }

        public async Task BulkInsert(IEnumerable<UserEntity> userEntityList)
        {
            await _context.User.AddRangeAsync(userEntityList);
            await _context.SaveChangesAsync();
        }


    }
}