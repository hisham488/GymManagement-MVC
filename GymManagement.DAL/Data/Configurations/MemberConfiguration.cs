using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.DAL.Data.Configurations
{
    internal class MemberConfiguration: GymUserConfiguration<Member> , IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x => x.CreatedAt)
                   .HasColumnName("JoinDate")
                   .HasDefaultValueSql("GetDate()");

            base.Configure(builder);
        }

    }
}
