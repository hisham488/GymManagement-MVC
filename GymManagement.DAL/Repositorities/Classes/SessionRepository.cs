using GymManagement.DAL.Data.DbContexts;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositorities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Repositorities.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContexts _dbContexts;
        public SessionRepository(GymDbContexts dbContext) :base(dbContext)
        {
            _dbContexts = dbContext;
        }
        public async Task<IEnumerable<Session>> GetAllSessionWithTrainerAndCategory(CancellationToken ct = default)
        {
            var query = _dbContexts.Sessions.AsNoTracking().Include(x=>x.Trainer).Include(s=>s.Category);
            return await query.ToListAsync();
        }

        public async Task<int> GetCountOfBookedSloatsAsync(int sessionId, CancellationToken ct = default)
        {
            return await _dbContexts.Bookings.AsNoTracking().CountAsync(b=>b.SessionId == sessionId);
        }
    }
}
