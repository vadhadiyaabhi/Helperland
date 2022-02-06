using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Helperland.Models;

#nullable disable

namespace Helperland.Repository
{
    public partial class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
        public virtual DbSet<FavoriteAndBlocked> FavoriteAndBlockeds { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }
        public virtual DbSet<ServiceRequestAddress> ServiceRequestAddresses { get; set; }
        public virtual DbSet<ServiceRequestExtra> ServiceRequestExtras { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<Zipcode> Zipcodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-22USEBB; Initial Catalog=Helperland_tatvasoft;Integrated Security=true");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_State");
            });

            //modelBuilder.Entity<ContactU>(entity =>
            //{
            //    entity.Property(e => e.FileName).IsUnicode(false);
            //});

            modelBuilder.Entity<FavoriteAndBlocked>(entity =>
            {
                entity.HasOne(d => d.TargetUser)
                    .WithMany(p => p.FavoriteAndBlockedTargetUsers)
                    .HasForeignKey(d => d.TargetUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavoriteAndBlocked_User");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FavoriteAndBlockedUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavoriteAndBlocked_FavoriteAndBlocked");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasOne(d => d.RatingFromNavigation)
                    .WithMany(p => p.RatingRatingFromNavigations)
                    .HasForeignKey(d => d.RatingFrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rating_User");

                entity.HasOne(d => d.RatingToNavigation)
                    .WithMany(p => p.RatingRatingToNavigations)
                    .HasForeignKey(d => d.RatingTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rating_User1");

                entity.HasOne(d => d.ServiceRequest)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.ServiceRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rating_ServiceRequest");
            });

            modelBuilder.Entity<ServiceRequest>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ServiceProvider)
                    .WithMany(p => p.ServiceRequestServiceProviders)
                    .HasForeignKey(d => d.ServiceProviderId)
                    .HasConstraintName("FK_ServiceRequest_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ServiceRequestUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceRequest_User");
            });

            modelBuilder.Entity<ServiceRequestAddress>(entity =>
            {
                entity.HasOne(d => d.ServiceRequest)
                    .WithMany(p => p.ServiceRequestAddresses)
                    .HasForeignKey(d => d.ServiceRequestId)
                    .HasConstraintName("FK_ServiceRequestAddress_ServiceRequest");
            });

            modelBuilder.Entity<ServiceRequestExtra>(entity =>
            {
                entity.HasOne(d => d.ServiceRequest)
                    .WithMany(p => p.ServiceRequestExtras)
                    .HasForeignKey(d => d.ServiceRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceRequestExtra_ServiceRequest");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.TestName).IsUnicode(false);
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK_UserAddresses");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAddresses_User");
            });

            modelBuilder.Entity<Zipcode>(entity =>
            {
                entity.HasOne(d => d.City)
                    .WithMany(p => p.Zipcodes)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zipcode_City");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
