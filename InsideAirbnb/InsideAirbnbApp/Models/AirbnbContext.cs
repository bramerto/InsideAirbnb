using Microsoft.EntityFrameworkCore;

namespace InsideAirbnbApp.Models
{
    public partial class AirbnbContext : DbContext
    {
        public AirbnbContext(DbContextOptions<AirbnbContext> options) : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Calendar> Calendar { get; set; }
        public virtual DbSet<Listings> Listings { get; set; }
        public virtual DbSet<Neighbourhoods> Neighbourhoods { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(x => x.RoleId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(x => x.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(x => x.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(x => x.UserId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(x => x.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(x => new { x.LoginProvider, x.ProviderKey });

                entity.HasIndex(x => x.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(x => x.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(x => new { x.UserId, x.RoleId });

                entity.HasIndex(x => x.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(x => x.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(x => x.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(x => x.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(x => x.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(x => x.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.HasKey(x => new { x.ListingId, x.Date });

                entity.ToTable("calendar");

                entity.Property(e => e.ListingId).HasColumnName("listing_id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.AdjustedPrice)
                    .HasColumnName("adjusted_price")
                    .HasMaxLength(50);

                entity.Property(e => e.Available)
                    .IsRequired()
                    .HasColumnName("available")
                    .HasMaxLength(50);

                entity.Property(e => e.MaximumNights).HasColumnName("maximum_nights");

                entity.Property(e => e.MinimumNights).HasColumnName("minimum_nights");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.Calendar)
                    .HasForeignKey(x => x.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_calendar_listings");
            });

            modelBuilder.Entity<Listings>(entity =>
            {
                entity.ToTable("listings");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Availability365).HasColumnName("availability_365");

                entity.Property(e => e.CalculatedHostListingsCount).HasColumnName("calculated_host_listings_count");

                entity.Property(e => e.HostId).HasColumnName("host_id");

                entity.Property(e => e.HostName).HasColumnName("host_name");

                entity.Property(e => e.LastReview).HasColumnName("last_review");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MinimumNights).HasColumnName("minimum_nights");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Neighbourhood)
                    .IsRequired()
                    .HasColumnName("neighbourhood");

                entity.Property(e => e.NeighbourhoodGroup).HasColumnName("neighbourhood_group");

                entity.Property(e => e.NumberOfReviews).HasColumnName("number_of_reviews");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReviewsPerMonth).HasColumnName("reviews_per_month");

                entity.Property(e => e.RoomType)
                    .IsRequired()
                    .HasColumnName("room_type");
            });

            modelBuilder.Entity<Neighbourhoods>(entity =>
            {
                entity.ToTable("neighbourhoods");

                entity.HasIndex(x => x.ListingId)
                    .HasName("IX_neighbourhoods");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ListingId).HasColumnName("listing_id");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.Neighbourhoods)
                    .HasForeignKey(x => x.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__neighbourhoods_listings");
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.ToTable("reviews");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ListingId).HasColumnName("listing_id");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(x => x.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reviews_listings");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
