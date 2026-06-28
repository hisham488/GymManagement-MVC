using GymManagement.BLL.ViewModels.SessionViewModel;
namespace GymManagement.BLL.Services.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<SessionViewModel>>GetAllSessionsAsync(CancellationToken ct);
        Task GetAllSessions();
    }
}
