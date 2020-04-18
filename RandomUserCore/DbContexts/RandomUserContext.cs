

using Microsoft.EntityFrameworkCore;

namespace RandomUserCore.Models
{
    public class RandomUserContext : DbContext
    {
        public RandomUserContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<UserEntity> User { get; set; }
        public virtual DbSet<ImageDetailEntity> ImageDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "3.1.3-servicing-1");

            modelBuilder.Entity<UserEntity>(entity =>
          {
              entity.HasKey(e => e.Id);
              entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
              entity.Property(e => e.FirstName)
                 .IsRequired()
                 .HasMaxLength(50);
              entity.Property(e => e.LastName).HasMaxLength(50);
              entity.Property(e => e.PhoneNumber).HasMaxLength(50);
              entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
              entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("getdate()");
              entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

              entity.HasIndex(e => e.Email).HasName("UQ__EmailAddress")
                                                 .IsUnique();
              entity.Property(e => e.Email)
                                  .HasMaxLength(512)
                                  .IsRequired();


              entity.HasOne(e => e.ImageDetail)
                               .WithOne(e => e.User)
                               .HasForeignKey<ImageDetailEntity>(e => e.UserId)
                               .OnDelete(DeleteBehavior.ClientSetNull)
                               .HasConstraintName("FK_Image_User")
                               .IsRequired(false);


          });

            modelBuilder.Entity<ImageDetailEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            entity.Property(e => e.Original)
                 .IsRequired()
                 .HasMaxLength(512);
            entity.Property(e => e.Thumbnail).HasMaxLength(512);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("getdate()");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });


        }
    }
}