using Microsoft.EntityFrameworkCore;

namespace InsideAirbnbApp.Models
{
    public partial class AirbnbContext : DbContext
    {
        public AirbnbContext(DbContextOptions<AirbnbContext> options) : base(options)
        {
        }

        public virtual DbSet<Calendar> Calendar { get; set; }
        public virtual DbSet<Hosts> Hosts { get; set; }
        public virtual DbSet<Listings> Listings { get; set; }
        public virtual DbSet<Neighbourhoods> Neighbourhoods { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<SummaryListings> SummaryListings { get; set; }
        public virtual DbSet<SummaryReviews> SummaryReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("calendar");

                entity.Property(e => e.Available)
                    .IsRequired()
                    .HasColumnName("available")
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ListingId).HasColumnName("listing_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Listing)
                    .WithMany()
                    .HasForeignKey(x => x.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_calendar_listings");
            });

            modelBuilder.Entity<Hosts>(entity =>
            {
                entity.ToTable("hosts");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.About).HasColumnName("about");

                entity.Property(e => e.AcceptanceRate).HasColumnName("acceptance_rate");

                entity.Property(e => e.IsSuperhost).HasColumnName("is_superhost");

                entity.Property(e => e.ListingsCount).HasColumnName("listings_count");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Neighbourhood).HasColumnName("neighbourhood");

                entity.Property(e => e.PictureUrl).HasColumnName("picture_url");

                entity.Property(e => e.ResponseRate).HasColumnName("response_rate");

                entity.Property(e => e.ResponseTime).HasColumnName("response_time");

                entity.Property(e => e.Since).HasColumnName("since");

                entity.Property(e => e.ThumbnailUrl).HasColumnName("thumbnail_url");

                entity.Property(e => e.TotalListingsCount).HasColumnName("total_listings_count");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url");

                entity.Property(e => e.Verifications).HasColumnName("verifications");
            });

            modelBuilder.Entity<Listings>(entity =>
            {
                entity.ToTable("listings");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Accommodates).HasColumnName("accommodates");

                entity.Property(e => e.Availability30).HasColumnName("availability_30");

                entity.Property(e => e.Availability365).HasColumnName("availability_365");

                entity.Property(e => e.Availability60).HasColumnName("availability_60");

                entity.Property(e => e.Availability90).HasColumnName("availability_90");

                entity.Property(e => e.Bathrooms).HasColumnName("bathrooms");

                entity.Property(e => e.Bedrooms).HasColumnName("bedrooms");

                entity.Property(e => e.Beds).HasColumnName("beds");

                entity.Property(e => e.CalendarUpdated).HasColumnName("calendar_updated");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.CleaningFee).HasColumnName("cleaning_fee");

                entity.Property(e => e.CountryCode).HasColumnName("country_code");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.ExtraPeoplePrice).HasColumnName("extra_people_price");

                entity.Property(e => e.FirstReview).HasColumnName("first_review");

                entity.Property(e => e.GuestsIncluded).HasColumnName("guests_included");

                entity.Property(e => e.HasAvailability).HasColumnName("has_availability");

                entity.Property(e => e.HostId).HasColumnName("host_id");

                entity.Property(e => e.LastReview).HasColumnName("last_review");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(21, 19)");

                entity.Property(e => e.ListingUrl)
                    .IsRequired()
                    .HasColumnName("listing_url");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(19, 18)");

                entity.Property(e => e.MaximumNights).HasColumnName("maximum_nights");

                entity.Property(e => e.MediumUrl).HasColumnName("medium_url");

                entity.Property(e => e.MinimumNights).HasColumnName("minimum_nights");

                entity.Property(e => e.MonthlyPrice).HasColumnName("monthly_price");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.NeighbourhoodId).HasColumnName("neighbourhood_id");

                entity.Property(e => e.NumberOfReviews).HasColumnName("number_of_reviews");

                entity.Property(e => e.PictureUrl)
                    .IsRequired()
                    .HasColumnName("picture_url");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.PropertyType).HasColumnName("property_type");

                entity.Property(e => e.ReviewScoresAccuracy).HasColumnName("review_scores_accuracy");

                entity.Property(e => e.ReviewScoresCheckin).HasColumnName("review_scores_checkin");

                entity.Property(e => e.ReviewScoresCleanliness).HasColumnName("review_scores_cleanliness");

                entity.Property(e => e.ReviewScoresCommunication).HasColumnName("review_scores_communication");

                entity.Property(e => e.ReviewScoresLocation).HasColumnName("review_scores_location");

                entity.Property(e => e.ReviewScoresRating).HasColumnName("review_scores_rating");

                entity.Property(e => e.ReviewScoresValue).HasColumnName("review_scores_value");

                entity.Property(e => e.ReviewsPerMonth).HasColumnName("reviews_per_month");

                entity.Property(e => e.RoomType).HasColumnName("room_type");

                entity.Property(e => e.SecurityDeposit).HasColumnName("security_deposit");

                entity.Property(e => e.SquareFeet).HasColumnName("square_feet");

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .HasColumnName("summary");

                entity.Property(e => e.ThumbnailUrl).HasColumnName("thumbnail_url");

                entity.Property(e => e.WeeklyPrice).HasColumnName("weekly_price");

                entity.Property(e => e.XlPictureUrl).HasColumnName("xl_picture_url");

                entity.Property(e => e.Zipcode).HasColumnName("zipcode");

                entity.HasOne(d => d.Host)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(x => x.HostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_listings_hosts");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Listings)
                    .HasForeignKey<Listings>(x => x.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_listings_summary-listings");

                entity.HasOne(d => d.Neighbourhood)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(x => x.NeighbourhoodId)
                    .HasConstraintName("FK_listings_neighbourhoods");
            });

            modelBuilder.Entity<Neighbourhoods>(entity =>
            {
                entity.ToTable("neighbourhoods");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Neighbourhood)
                    .IsRequired()
                    .HasColumnName("neighbourhood")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.ToTable("reviews");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasColumnName("comments");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ListingId).HasColumnName("listing_id");

                entity.Property(e => e.ReviewerId).HasColumnName("reviewer_id");

                entity.Property(e => e.ReviewerName)
                    .IsRequired()
                    .HasColumnName("reviewer_name")
                    .HasMaxLength(150);

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(x => x.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reviews_listings");
            });

            modelBuilder.Entity<SummaryListings>(entity =>
            {
                entity.ToTable("summary-listings");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Availability365).HasColumnName("availability_365");

                entity.Property(e => e.CalculatedHostListingsCount).HasColumnName("calculated_host_listings_count");

                entity.Property(e => e.HostId).HasColumnName("host_id");

                entity.Property(e => e.HostName).HasColumnName("host_name");

                entity.Property(e => e.LastReview).HasColumnName("last_review");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(21, 19)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(19, 18)");

                entity.Property(e => e.MinimumNights).HasColumnName("minimum_nights");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.NeighbourhoodId).HasColumnName("neighbourhood_id");

                entity.Property(e => e.NumberOfReviews).HasColumnName("number_of_reviews");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReviewsPerMonth).HasColumnName("reviews_per_month");

                entity.Property(e => e.RoomType).HasColumnName("room_type");

                entity.HasOne(d => d.Neighbourhood)
                    .WithMany(p => p.SummaryListings)
                    .HasForeignKey(x => x.NeighbourhoodId)
                    .HasConstraintName("FK_summary-listings_neighbourhoods");
            });

            modelBuilder.Entity<SummaryReviews>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("summary-reviews");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ListingId).HasColumnName("listing_id");

                entity.HasOne(d => d.Listing)
                    .WithMany()
                    .HasForeignKey(x => x.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_summary-reviews_listings");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
