namespace GymManagement.DAL.Data.Configurations;

using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(50);
       
          builder.Property(x => x.Email)
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.HasIndex(X=>X.Email).IsUnique();
        builder.HasIndex(X=>X.Phone).IsUnique();
        builder.ToTable(tb => 
        {
        tb.HasCheckConstraint("EmailCheck", "Email LIKE '_%@_%._%'");
        tb.HasCheckConstraint("_PhoneCheck", "Phone LIKE '010%' or Phone LIKE '011%' or Phone LIKE '015%' or Phone LIKE '012%'");
        });

        builder.OwnsOne(x => x.Address, Address => {
        
        Address.Property(x => x.Street)
               .HasColumnName("Street")
               .HasColumnType("nvarchar")
               .HasMaxLength(30);   

        Address.Property(x => x.City)
               .HasColumnName("City")
               .HasColumnType("nvarchar")
               .HasMaxLength(30);   

            Address.Property(x => x.BuildingNumber)
             .HasColumnName("BuildingNumber")
               .HasColumnType("nvarchar")
               .HasMaxLength(10);
        });
    }
}
