using GymManagement.DAL.Data.DbContexts;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositorities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace GymManagement.DAL.Repositorities.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDbContexts _dbcontext;
        private readonly DbSet<TEntity> _Set;
        public GenericRepository(GymDbContexts dbcontext)
        {
            _dbcontext = dbcontext;
            _Set = dbcontext.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
             _Set.Add(entity);

        }
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        {
            return _Set.AsNoTracking().AnyAsync(predicate, ct);
        }
        public void Delete(TEntity entity)
        {
            _Set.Remove(entity);
        }
        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false, CancellationToken ct = default)
        {
            IQueryable<TEntity> query = tracking ? _Set : _Set.AsNoTracking();
            return await query.FirstOrDefaultAsync(predicate, ct);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false, CancellationToken ct = default)
        {
           IQueryable<TEntity> query = tracking ? _Set : _Set.AsNoTracking();
            return await query.ToListAsync(ct);
        }
        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default) => await _Set.FindAsync(id, ct);

        public async Task<int> UpdateAsync(TEntity entity)
        {
            _Set.Update(entity);
            return await _dbcontext.SaveChangesAsync();
        }
        public void Update(TEntity entity)
        {
            _Set.Update(entity);          
        }
    }
}
