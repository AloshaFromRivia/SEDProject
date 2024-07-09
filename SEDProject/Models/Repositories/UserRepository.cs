
using Microsoft.EntityFrameworkCore;
using SEDProject.Models.Database;
using SEDProject.Models.Entities;
using System.Linq.Expressions;

namespace SEDProject.Models.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<EntityState> CreateAsync(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.State;
        }

        public async Task<EntityState> UpdateAsync(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return result.State;
        }

        public async Task<EntityState> RemoveAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            var participants = _context.Participants.Where(p=> p.User == user);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = _context.Users.Remove(user);
            //remove linked objects
            _context.Participants.RemoveRange(participants);

            await _context.SaveChangesAsync();
            return result.State;
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync(Expression<Func<User, bool>> filter)
        {
            return await _context.Users.Where(filter).ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> filter)
        {
            return await _context.Users.Where(filter).FirstOrDefaultAsync();
        }
    }
}
