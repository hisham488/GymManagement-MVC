using GymManagement.BLL.ViewModels.MemberViewModels;
namespace GymManagement.BLL.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default);
        Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default);
        Task<MemberViewModel?> GetMemberDetailsByIdAsync(int Id, CancellationToken ct = default);
        Task<HealthRecordViewModel> GetMemberHealthRecordAsync(int memberId, CancellationToken ct = default);
        Task<MemberToUpDateViewModel?> GetMemberToUpDateAsync(int memberId, CancellationToken ct = default);
        Task<bool> UpDateMemberDetailsAsync(int id, MemberToUpDateViewModel model, CancellationToken ct = default);
        Task<bool> RemoveMemberAsync(int memberId, CancellationToken ct = default);
    }
}