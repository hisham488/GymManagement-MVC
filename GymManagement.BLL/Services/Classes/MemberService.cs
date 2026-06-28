using GymManagement.BLL.Services.Interfaces;
using GymManagement.BLL.ViewModels.MemberViewModels;
using GymManagement.DAL.Data.DbContexts;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositorities.Interfaces;

namespace GymManagement.BLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            //Check Email
            var EmailExist = await _unitOfWork.GetRepository<Member>().AnyAsync(m => m.Email == model.Email, ct);
            //Check Phone
            var phoneExist = await _unitOfWork.GetRepository<Member>().AnyAsync(m => m.Phone == model.Phone, ct);
            //Email Or Phone Exist => return false
            if (EmailExist || phoneExist) return false;
            //Else Return true Add Member
            var member = new Member()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Address = new Address()
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },
                HealthRecord = new HealthRecord()
                {
                    BloodType = model.HealthRecordViewModel.BloodType,
                    weight = model.HealthRecordViewModel.Weight,
                    height = model.HealthRecordViewModel.Height,
                    Note = model.HealthRecordViewModel.Note
                },
            };
            _unitOfWork.GetRepository<Member>().Add(member);
            var result = await _unitOfWork.SaveChangesAsync(ct);
            return result > 0;
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default)
        {
            var members = await _unitOfWork.GetRepository<Member>().GetAllAsync(ct: ct);
            if (!members.Any()) return [];

            var memberViewModels = members.Select(member => new MemberViewModel
            {
                Email = member.Email,
                Gender = member.Gender.ToString(),
                Name = member.Name,
                Phone = member.Phone,
                Photo = member.Photo,
                Id = member.Id
            });
            return memberViewModels;
        }

        public async Task<MemberViewModel?> GetMemberDetailsByIdAsync(int MemberId, CancellationToken ct = default)
        {
            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(MemberId, ct);
            if (member == null) return null;

            var model = new MemberViewModel()
            {
                Name = member.Name,
                Phone = member.Phone,
                DateOfBirth = member.DateOfBirth.ToString(),
                Gender = member.Gender.ToString(),
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}"
            };

            var activeMembership = await _unitOfWork.GetRepository<MemberShip>().FirstOrDefaultAsync(x => x.MemberId == MemberId && x.EndDate > DateTime.Now);

            if (activeMembership is not null)
            {
                var activePlan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(activeMembership.PlanId, ct);
                model.PlanName = activePlan?.Name;
                model.MembershipStartDate = activeMembership.CreatedAt.ToString();
                model.MembershipEndDate = activeMembership.EndDate.ToString();
            }
            return model;

        }

        public async Task<HealthRecordViewModel> GetMemberHealthRecordAsync(int memberId, CancellationToken ct = default)
        {
            var record = await _unitOfWork.GetRepository<HealthRecord>().FirstOrDefaultAsync(x => x.MemberId == memberId, ct: ct);
            if (record == null)
                throw new Exception("Health record not found");
            else
                return new HealthRecordViewModel()
                {
                    Weight = record.weight,
                    BloodType = record.BloodType,
                    Height = record.height,
                    Note = record.Note,
                };
        }

        public async Task<MemberToUpDateViewModel?> GetMemberToUpDateAsync(int memberId, CancellationToken ct = default)
        {
            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(memberId, ct);
            if (member == null) return null;
            else
                return new MemberToUpDateViewModel()
                {
                    Name = member.Name,
                    Phone = member.Phone,
                    Email = member.Email,
                    BuildingNumber = member.Address.BuildingNumber,
                    City = member.Address.City,
                    Street = member.Address.Street,
                    Photo = member.Photo
                };
        }

        public async Task<bool> RemoveMemberAsync(int memberId, CancellationToken ct = default)
        {
            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(memberId, ct);
             if(member == null) return false;
             var hasFutureBooking= await _unitOfWork.GetRepository<Booking>().AnyAsync(b=>b.MemberId == memberId && b.Session.StartDate > DateTime.Now , ct);
            if(hasFutureBooking) return false;
             _unitOfWork.GetRepository<Member>().Delete(member);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;

        }

        public async Task<bool> UpDateMemberDetailsAsync(int id, MemberToUpDateViewModel model, CancellationToken ct = default)
        {
            {
                var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(id, ct);
                if (member == null) return false;
                var emailExists = await _unitOfWork.GetRepository<Member>().AnyAsync(m => m.Email == model.Email && m.Id != m.Id);
                var phoneExists = await _unitOfWork.GetRepository<Member>().AnyAsync(m => m.Email == model.Email && m.Id != m.Id);
                if (emailExists || phoneExists) return false;

                member.Email = model.Email;
                member.Phone = model.Phone;
                member.Address.City = model.City;
                member.Address.BuildingNumber = model.BuildingNumber;
                member.Address.Street = model.Street;
                member.UpdateAt = DateTime.Now;

                 _unitOfWork.GetRepository<Member>().Update(member);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
        }
    }
}
