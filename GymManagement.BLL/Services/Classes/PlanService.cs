using GymManagement.BLL.ViewModels.PlanViewModels;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositorities.Interfaces;
using GymManagementSystem.BLL.Services.Interfaces;
using GymManagementSystem.BLL.ViewModels.PlanViewModels;
namespace GymManagement.BLL.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PlanViewModel>> GetAllPlansAsync(CancellationToken ct = default)
        {
            var plans = await _unitOfWork.GetRepository<Plan>().GetAllAsync(ct: ct);

            return plans.Select(p => new PlanViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description ?? string.Empty,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive,
                Price = p.Price
            });
        }
        public async Task<PlanViewModel?> GetPlanByIdAsync(int planId, CancellationToken ct = default)
        {
            var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(planId, ct);

            if (plan is null)
                return null;

            return new PlanViewModel()
            {
                Name = plan.Name,
                Description = plan.Description ?? string.Empty,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                IsActive = plan.IsActive
            };
        }
        public async Task<UpdatePlanViewModel?> GetPlanToUpdateAsync(int planId, CancellationToken ct = default)
        {
            var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(planId, ct);

            if (plan is null || !plan.IsActive)
                return null;

            if (await HasActiveMembershipsAsync(planId, ct))
                return null;

            return new UpdatePlanViewModel()
            {
                PlanName = plan.Name,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                Description = plan.Description ?? string.Empty,
            };
        }
        public async Task<bool> ToggleActivationAsync(int planId, CancellationToken ct = default)
        {
            var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(planId, ct);

            if (plan is null)
                return false;

            plan.IsActive = !plan.IsActive;
            plan.UpdatedAt = DateTime.Now;

            _unitOfWork.GetRepository<Plan>().Update(plan);

            var result = await _unitOfWork.SaveChangesAsync(ct);

            return result > 0;
        }

        public async Task<bool> UpdatePlanAsync(int id, UpdatePlanViewModel model, CancellationToken ct = default)
        {
            var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(id, ct);

            if (plan is null)
                return false;

            if (await HasActiveMembershipsAsync(id, ct))
                return false;

            plan.DurationDays = model.DurationDays;
            plan.Price = model.Price;
            plan.Description = model.Description;
            plan.UpdatedAt = DateTime.Now;

            _unitOfWork.GetRepository<Plan>().Update(plan);

            var result = await _unitOfWork.SaveChangesAsync(ct);

            return result > 0;
        }
        private async Task<bool> HasActiveMembershipsAsync(int planId, CancellationToken ct)
        {
            return await _unitOfWork
                .GetRepository<MemberShip>()
                .AnyAsync(m => m.PlanId == planId &&
                               m.EndDate > DateTime.Now, ct);
        }
    }
}
