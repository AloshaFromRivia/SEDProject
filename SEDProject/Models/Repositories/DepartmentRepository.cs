using Microsoft.EntityFrameworkCore;
using SEDProject.Models.Database;
using SEDProject.Models.Entities;
using System.Linq.Expressions;

namespace SEDProject.Models.Repositories
{
    public class DepartmentRepository : IRepository<Department>
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetAsync(Guid id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<EntityState> CreateAsync(Department entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await _context.Departments.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.State;
        }

        public async Task<EntityState> UpdateAsync(Department entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = _context.Departments.Update(entity);
            await _context.SaveChangesAsync();
            return result.State;
        }

        public async Task<EntityState> RemoveAsync(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return result.State;
        }

        public async Task<IReadOnlyCollection<Department>> GetAllAsync(Expression<Func<Department, bool>> filter)
        {
            return await _context.Departments.Where(filter).ToListAsync();
        }

        public async Task<Department> GetAsync(Expression<Func<Department, bool>> filter)
        {
            return await _context.Departments.Where(filter).FirstOrDefaultAsync();
        }
    }
}