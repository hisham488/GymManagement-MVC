using GymManagement.DAL.Data.DbContexts;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositorities.Interfaces;

namespace GymManagement.DAL.Repositorities.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContexts _dbContexts;
        private readonly Dictionary<string , object> _repositories = [];
        public UnitOfWork(GymDbContexts dbContex ,ISessionRepository sessionRepository) 
        { 
        _dbContexts = dbContex;
            SessionRepository = sessionRepository;
        }
        public ISessionRepository SessionRepository { get;  }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
        var typeName= typeof(TEntity).Name;
            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity>) value;
            else
            {
                var repo= new GenericRepository<TEntity>(_dbContexts);
                _repositories[typeName]=repo;
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _dbContexts.SaveChangesAsync(ct); 
            
        
    }
}
