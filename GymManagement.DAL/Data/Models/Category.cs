namespace GymManagement.DAL.Data.Models
{
    public class Category : BaseEntity
    {
        public string? CategoryName { get; set; }
        public ICollection<Session>  sessions { get; set; } = default!;
    }
}
