using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GymManagement.DAL.Data.Configurations
{
    internal class MemberShipConfiguration : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder.HasKey(x => x.MemberId);
            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
