using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BAISTGolfClub.Data.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BAISTGolfClub.Data.DBContext
{
    public partial class BAISTGolfClubContext : DbContext
    {
        public BAISTGolfClubContext()
        {
        }

        public BAISTGolfClubContext(DbContextOptions<BAISTGolfClubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Membership> Membership { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<StandingReservation> StandingReservation { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=BAISTGolfClub_vtalwar1;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasKey(e => e.MembershipNumber)
                    .HasName("PK_Membership_1");

                entity.Property(e => e.MembershipNumber).ValueGeneratedNever();

                entity.Property(e => e.MembershipType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.ReservationId).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.ResevationNumber).ValueGeneratedOnAdd();
                entity.Property(e => e.ResevationNumber).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.HasOne(d => d.StandingReservation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.StandingReservationId)
                    .HasConstraintName("FK_Reservation_StandingReservation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_User");
            });

            modelBuilder.Entity<StandingReservation>(entity =>
            {
                entity.Property(e => e.StandingReservationId).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StandingReservationNumber).ValueGeneratedOnAdd();

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.StandingReservationApprovedByNavigation)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_StandingReservation_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StandingReservationUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StandingReservation_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.MembershipNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Membership");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
