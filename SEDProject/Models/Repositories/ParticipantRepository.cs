using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SEDProject.Models.Database;
using SEDProject.Models.Entities;
using System.Linq.Expressions;

namespace SEDProject.Models.Repositories
{
    public class ParticipantRepository : IRepository<Participant>
    {
        private readonly AppDbContext _context;

        public ParticipantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Participant>> GetAllAsync()
        {
            return await _context.Participants
                .Include(u=>u.User)
                .Include(d=>d.Department)
                .ToListAsync();
        }

        public async Task<Participant> GetAsync(Guid id)
        {
            return await _context.Participants
                 .Include(u => u.User)
                 .Include(d => d.Department)
                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<EntityState> CreateAsync(Participant entity)
        {

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (_context.Participants.Any(_context => _context.Id == entity.Id))
                throw new ArgumentException();

            var result = await _context.Participants.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.State;
        }

        public async Task<EntityState> UpdateAsync(Participant entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = _context.Participants.Update(entity);
            await _context.SaveChangesAsync();
            return result.State;
        }

        public async Task<EntityState> RemoveAsync(Guid id)
        {
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();
            return result.State;
        }

        public async Task<IReadOnlyCollection<Participant>> GetAllAsync(Expression<Func<Participant, bool>> filter)
        {
            return await _context.Participants.Where(filter)
                 .Include(u => u.User)
                 .Include(d => d.Department)
                 .ToListAsync();
        }

        public async Task<Participant> GetAsync(Expression<Func<Participant, bool>> filter)
        {
            return await _context.Participants.Where(filter)
                 .Include(u => u.User)
                 .Include(d => d.Department)
                 .FirstOrDefaultAsync();
        }
    }
}
