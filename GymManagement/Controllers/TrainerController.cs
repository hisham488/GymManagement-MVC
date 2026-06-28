using GymManagement.BLL.Services.Interfaces;
using GymManagement.BLL.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.PL.Controllers
{
    public class TrainersController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainersController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        // GET: Index
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var trainers = await _trainerService.GetAllTrainersAsync(ct);
            return View(trainers);
        }
        // GET: Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainerViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _trainerService.CreateTrainerAsync(model, ct);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer created successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Trainer failed to create";
            return View(model);
        }

        // GET: Details
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var trainer = await _trainerService.GetTrainerDetailsAsync(id, ct);

            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }

        // GET: Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var trainer = await _trainerService.GetTrainerToUpdateAsync(id, ct);

            if (trainer == null)
                return RedirectToAction(nameof(Index));

            return View(trainer);
        }

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TrainerToUpdateViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _trainerService.UpdateTrainerDetailsAsync(id, model, ct);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer failed to update";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var trainer = await _trainerService.GetTrainerDetailsAsync(id, ct);

            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }

        // Optional
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            var result = await _trainerService.RemoveTrainerAsync(id, ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To delete Trainer";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
