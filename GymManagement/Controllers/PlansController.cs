using Microsoft.AspNetCore.Mvc;
using GymManagementSystem.BLL.Services.Interfaces;
using GymManagementSystem.BLL.ViewModels.PlanViewModels;

namespace GymManagement.Controllers
{
    public class PlansController : Controller
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        // GET: Plans
        public async Task<IActionResult> Index(CancellationToken ct)
            => View(await _planService.GetAllPlansAsync(ct));

        // GET: Plans/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var plan = await _planService.GetPlanByIdAsync(id, ct);

            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }

        // GET: Plans/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var plan = await _planService.GetPlanToUpdateAsync(id, ct);

            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan cannot be updated";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }

        // POST: Plans/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(
            int id,
            UpdatePlanViewModel model,
            CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _planService.UpdatePlanAsync(id, model, ct);

            if (result)
                TempData["SuccessMessage"] = "Plan updated successfully";
            else
                TempData["ErrorMessage"] = "Failed to update plan";

            return RedirectToAction(nameof(Index));
        }

        // POST: Plans/ToggleActivation/5
        [HttpPost]
        public async Task<IActionResult> ToggleActivation(
            int id,
            CancellationToken ct)
        {
            var result = await _planService.ToggleActivationAsync(id, ct);

            if (result)
                TempData["SuccessMessage"] = "Plan status changed successfully";
            else
                TempData["ErrorMessage"] = "Failed to change plan status";

            return RedirectToAction(nameof(Index));
        }
    }
}