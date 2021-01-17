using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Commsights.Data.Models
{
    public partial class CommsightsContext : DbContext
    {
        public CommsightsContext()
        {
        }

        public CommsightsContext(DbContextOptions<CommsightsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<Membership> Membership { get; set; }
        public virtual DbSet<MembershipPermission> MembershipPermission { get; set; }

        public virtual DbSet<MembershipAccessHistory> MembershipAccessHistory { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductProperty> ProductProperty { get; set; }
        public virtual DbSet<ProductSearch> ProductSearch { get; set; }
        public virtual DbSet<ProductSearchProperty> ProductSearchProperty { get; set; }
        public virtual DbSet<EmailStorage> EmailStorage { get; set; }
        public virtual DbSet<EmailStorageProperty> EmailStorageProperty { get; set; }
        public virtual DbSet<ReportMonthly> ReportMonthly { get; set; }
        public virtual DbSet<ReportMonthlyProperty> ReportMonthlyProperty { get; set; }
        public virtual DbSet<ProductPermission> ProductPermission { get; set; }
        public virtual DbSet<BaiVietUploadCount> BaiVietUploadCount { get; set; }
        public virtual DbSet<BaiVietUpload> BaiVietUpload { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Commsights.Data.Helpers.AppGlobal.ConectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
