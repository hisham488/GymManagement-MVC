namespace GymManagement.DAL.Data.Models
{
    public class HealthRecord :BaseEntity
    {
        public decimal height { get; set; }
        public decimal weight { get; set; }
        public string? BloodType { get; set; }
        public string? Note { get; set; }
        public Member Member { get; set; } = default!; 
        public int MemberId { get; set; }
    }
}
