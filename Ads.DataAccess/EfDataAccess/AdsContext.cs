using Ads.DataAccess.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ads.DataAccess.EfDataAccess
{
    public class AdsContext : IdentityDbContext<ApplicationUser>
    {
        public AdsContext()
        {

        }
        public AdsContext(DbContextOptions<AdsContext> options) : base(options)
        {

        }
        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Follower> Followers { get; set; }

        public virtual DbSet<Offer> Offers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-CKGIAK6;Initial Catalog=Ads;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Ad>(entity =>
            {
                entity.HasKey(key => key.Id);
                entity.HasIndex(index => index.Subject);
                entity.Property(prop => prop.AddedDateTime)
                    .HasColumnType("datetime")
                    .IsRequired();
                entity.Property(prop => prop.Description)
                    .HasMaxLength(500);

                entity.HasOne(x => x.Category)
                    .WithMany(x => x.Ads)
                    .HasForeignKey(x => x.CategoryId)
                    .HasConstraintName("FK_Ad_Categories")
                    .IsRequired();
                entity.HasOne(x => x.User)
                    .WithMany(x => x.Ads)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_Ad_Users")
                    .IsRequired();

            });

            builder.Entity<Category>(entity =>
            {
                entity.HasKey(key => key.Id);
                entity.HasIndex(index => index.Name);
            });

            builder.Entity<Follower>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User)
                    .WithMany(x => x.Followers)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_Follower_Users")
                    .IsRequired();
                e.HasOne(x => x.Ad)
                    .WithMany(x => x.Followers)
                    .HasForeignKey(x => x.AdId)
                    .HasConstraintName("FK_Follower_Ad")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Offer>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Amount)
                    .HasColumnType("decimal(13,2)")
                    .IsRequired();
                e.HasOne(x => x.User)
                    .WithMany(x => x.Offers)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_Offer_Users")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(x => x.Ad)
                    .WithMany(x => x.Offers)
                    .HasForeignKey(x => x.AdId)
                    .HasConstraintName("FK_Offer_Ads")
                    .IsRequired();
            });

            builder.Entity<Comment>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Comments)
                    .HasMaxLength(1024)
                    .IsRequired();
                e.HasOne(x => x.Ad)
                    .WithMany(x => x.Comments)
                    .HasForeignKey(x => x.AdId);
                e.HasOne(x => x.User)
                    .WithMany(x => x.Comments)
                    .HasForeignKey(x => x.UserId);
            });


            base.OnModelCreating(builder);
        }
    }
}