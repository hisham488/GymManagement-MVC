using Microsoft.AspNetCore.Mvc;
using GymManagement.BLL.Services.Interfaces;
using GymManagement.BLL.ViewModels.MemberViewModels;

namespace GymManagement.PL.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: Members/Index
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var members = await _memberService.GetAllMembersAsync(ct);
            return View("~/Views/Members/Index.cshtml", members);
        }

        // GET: Members/MemberDetails/{id}
        public async Task<IActionResult> MemberDetails(int id, CancellationToken ct)
        {
            var member = await _memberService.GetMemberDetailsByIdAsync(id, ct);

            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        // GET: Members/HealthRecordDetails/{id}
        public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken ct)
        {
            var result = await _memberService.GetMemberHealthRecordAsync(id, ct);

            if (result is null)
            {
                TempData["ErrorMessage"] = "Health Record Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(result);
        }

        #region Create Member

        // GET: Members/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _memberService.CreateMemberAsync(model, ct);

            if (result)
                TempData["SuccessMessage"] = "Member Created Successfully";
            else
                TempData["ErrorMessage"] = "Member Creation Failed";

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Edit Member

        // GET: Members/EditMember/{id}
        [HttpGet]
        public async Task<IActionResult> EditMember(int id, CancellationToken ct)
        {
            var member = await _memberService.GetMemberToUpDateAsync(id, ct);

            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        // POST: Members/EditMember/{id}
        [HttpPost]
        public async Task<IActionResult> EditMember(int id, MemberToUpDateViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _memberService.UpDateMemberDetailsAsync(id, model, ct);

            if (result)
                TempData["SuccessMessage"] = "Member Updated Successfully";
            else
                TempData["ErrorMessage"] = "Failed To Update Member";

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete Member

        // GET: Members/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var member = await _memberService.GetMemberDetailsByIdAsync(id, ct);

            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // POST: Members/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]int id, CancellationToken ct)
        {
            var result = await _memberService.RemoveMemberAsync(id, ct);

            if (result)
                TempData["SuccessMessage"] = "Member Deleted Successfully";
            else
                TempData["ErrorMessage"] = "Failed To Delete Member";

            return RedirectToAction(nameof(Index));
        }

        #endregion
    } 
}