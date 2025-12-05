using App.Persistance.Repositories;
using APP.Domain.Entities.UserDto;
using APP.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Service.UserServiceImp
{
    public interface IUserService
    {
        Task<User> Insert(User data);
        Task<User> Update(User data);
        Task<User> Delete(long id);
        Task<List<User>> GetAll();
        Task<User> GetById(long id);
        Task<User?> GetAsync(Expression<Func<User, bool>> predicate);
    }
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userrepository;
        private readonly AppDbContext _appDbContext;
        public UserService(IBaseRepository<User> userrepository, AppDbContext appDbContext)
        {
            _userrepository = userrepository ?? throw new ArgumentNullException(nameof(userrepository));
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        // INSERT
        public async Task<User> Insert(User data)
        {
            if (data == null)
                return new User();

            await _userrepository.Add(data);
            return data;
        }

        // UPDATE
        public async Task<User> Update(User data)
        {
            if (data == null)
                return new User();

            User? entity = await _userrepository.GetByIdAsync(data.Id);

            if (entity == null)
                return new User();

            // Update record
            var updated = await _userrepository.UpdateAsync(data);

            return updated;
        }

        // DELETE
        public async Task<User> Delete(long id)
        {
            User? entity = await _userrepository.GetByIdAsync(id);

            if (entity == null)
                return new User();

            await _userrepository.DeleteAsync(entity);

            return entity;
        }

        // GET ALL
        public async Task<List<User>> GetAll()
        {
            return await _userrepository.ToListAsync();
        }

        // GET BY ID
        public async Task<User> GetById(long id)
        {
            User? entity = await _userrepository.GetByIdAsync(id);

            if (entity == null)
                return new User();

            return entity;
        }
        public async Task<User?> GetAsync(Expression<Func<User, bool>> predicate)
        {
            return await _appDbContext.Users
           .Where(a => !a.Deleted)
           .FirstOrDefaultAsync(predicate);
        }
    }

}
