using GymManagement.BLL.Services.Interfaces;
using GymManagement.BLL.ViewModels.SessionViewModel;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositorities.Interfaces;

namespace GymManagement.BLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken ct)
        {
            var sessionRepo=_unitOfWork.SessionRepository;
            var sessions = await sessionRepo.GetAllSessionWithTrainerAndCategory(ct);
            if (sessions == null ||!sessions.Any()) return null;
            var mappSession = sessions.Select(x => new SessionViewModel()
            {
                Id = x.Id,
                Capacity = x.Capacity,
                TrainerName = x.Trainer.Name,
                Description =x.Description,
                EndDate = x.EndDate,
                StartDate = x.StartDate,
            });


            foreach (var session in mappSession)
            {
                session.AvailableSlots =  session.Capacity - await sessionRepo.GetCountOfBookedSloatsAsync(session.Id,ct); 
            }
            return mappSession;
        }

        public Task GetAllSessions()
        {
            throw new NotImplementedException();
        }
    }
}
