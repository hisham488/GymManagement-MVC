using GymManagement.DAL.Data.Models.Enums;

namespace GymManagement.DAL.Data.Models
{
    public class Trainer : GymUser
    {
        //HierDate = CreatedAt In BaseEntity
        public Specialties specialty { get; set; }
        public ICollection<Session> sessions  { get; set; } = default!;
    }
}
