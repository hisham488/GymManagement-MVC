
namespace GymManagement.DAL.Data.Models
{
    public class Member : GymUser
    {
        public string? Photo { get; set; }

        #region Relationships
        public HealthRecord HealthRecord { get; set; } = default!;
        public ICollection<MemberShip> MemberShips { get; set; } = default!;
        public ICollection<Booking> Bookings { get; set; } = default!;
        public DateTime EndDate { get; set; }

        #endregion
    }
}
