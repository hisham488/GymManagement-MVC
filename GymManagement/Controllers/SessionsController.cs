using GymManagement.BLL.Services.Interfaces;
using GymManagement.DAL.Repositorities.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionsController( ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var Session = await _sessionService.GetAllSessionsAsync(ct);
            return View(Session);
        }
    }
}
