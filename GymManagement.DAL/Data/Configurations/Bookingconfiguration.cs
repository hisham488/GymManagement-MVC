using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GymManagement.DAL.Data.Configurations
{
    internal class Bookingconfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Ignore(x=>x.MemberId);
            builder.Property(x => x.CreatedAt)
                .HasColumnName("bookingDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasKey(x => new { x.SessionId, x.MemberId });
          
        }
    }
}
