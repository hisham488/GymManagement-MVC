namespace GymManagement.DAL.Data.Models
{
    public class MemberShip : BaseEntity
    {
        public Member Member { get; set; } = default!;
        public int MemberId { get; set; }
        public Plan Plan { get; set; } = default!;
        public int PlanId { get; set; }
        // StartDate = CreatedAt of BaseEntity
        public DateTime EndDate { get; set; }
        public string Status => DateTime.Now > EndDate ? "Expired" : "Active";
        public bool IsActive => EndDate > DateTime.Now;
    }
}
