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
        public virtual DbSet<SubCategory> SubCategories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=AdsDB; Integrated Security=True;");
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
                entity.HasOne(x => x.SubCategory)
                    .WithMany(x => x.Ads)
                    .HasForeignKey(x => x.SubCategoryId)
                    .HasConstraintName("FK_Ad_SubCategories")
                    .IsRequired();
                entity.HasOne(x => x.User)
                    .WithMany(x => x.Ads)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_Ad_Users")
                    .IsRequired();

            });

            builder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(key => key.Id);
                entity.HasIndex(index => index.Name);
                entity.HasOne(x => x.Category)
                    .WithMany(x => x.SubCategories)
                    .HasForeignKey(x => x.CategoryId)
                    .HasConstraintName("FK_SubCategory_Categories")
                    .IsRequired();
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasKey(key => key.Id);
                entity.HasIndex(index => index.Name);
            });
            base.OnModelCreating(builder);
        }
    }
}