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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Commsights.Data.Helpers.AppGlobal.ConectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Config>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Action).HasMaxLength(4000);

                entity.Property(e => e.Code).HasMaxLength(4000);

                entity.Property(e => e.CodeName).HasMaxLength(4000);

                entity.Property(e => e.CodeNameSub).HasMaxLength(4000);

                entity.Property(e => e.Controller).HasMaxLength(4000);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.GroupName).HasMaxLength(4000);

                entity.Property(e => e.Icon).HasMaxLength(4000);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");


                entity.Property(e => e.Title).HasMaxLength(4000);

                entity.Property(e => e.Urlfull)
                    .HasColumnName("URLFull")
                    .HasMaxLength(4000);

                entity.Property(e => e.Urlsub)
                    .HasColumnName("URLSub")
                    .HasMaxLength(4000);
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Account).HasMaxLength(4000);

                entity.Property(e => e.Address).HasMaxLength(4000);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CitizenIdentification).HasMaxLength(4000);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(4000);

                entity.Property(e => e.FirstName).HasMaxLength(4000);

                entity.Property(e => e.FullName).HasMaxLength(4000);

                entity.Property(e => e.Guicode)
                    .HasColumnName("GUICode")
                    .HasMaxLength(4000);

                entity.Property(e => e.LastName).HasMaxLength(4000);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Passport).HasMaxLength(4000);

                entity.Property(e => e.Password).HasMaxLength(4000);

                entity.Property(e => e.Phone).HasMaxLength(4000);

                entity.Property(e => e.Points).HasColumnType("money");

                entity.Property(e => e.TaxCode).HasMaxLength(4000);
            });

            modelBuilder.Entity<MembershipPermission>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");               

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.MembershipId).HasColumnName("MembershipID");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");     
                
                entity.Property(e => e.Product).HasMaxLength(4000);
            });

            //modelBuilder.Entity<MembershipAccessHistory>(entity =>
            //{
            //    entity.Property(e => e.Id)
            //        .HasColumnName("ID")
            //        .ValueGeneratedNever();

            //    entity.Property(e => e.DateCreated).HasColumnType("datetime");

            //    entity.Property(e => e.DateUpdated).HasColumnType("datetime");


            //    entity.Property(e => e.DateTrack).HasColumnType("datetime");

            //    entity.Property(e => e.MembershipId).HasColumnName("MembershipID");

            //    entity.Property(e => e.URLFull).HasMaxLength(4000);
            //    entity.Property(e => e.Controller).HasMaxLength(4000);
            //    entity.Property(e => e.Action).HasMaxLength(4000);
            //    entity.Property(e => e.QueryString).HasMaxLength(4000);

            //    entity.Property(e => e.ParentId).HasColumnName("ParentID");
            //});

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Author).HasMaxLength(4000);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ContentMain).HasMaxLength(4000);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Image).HasMaxLength(4000);

                entity.Property(e => e.ImageThumbnail).HasMaxLength(4000);

                entity.Property(e => e.MetaDescription).HasMaxLength(4000);

                entity.Property(e => e.MetaKeyword).HasMaxLength(4000);

                entity.Property(e => e.MetaTitle).HasMaxLength(4000);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.PriceUnitId).HasColumnName("PriceUnitID");

                entity.Property(e => e.Tags).HasMaxLength(4000);

                entity.Property(e => e.Title).HasMaxLength(4000);

                entity.Property(e => e.Urlcode)
                    .HasColumnName("URLCode")
                    .HasMaxLength(4000);

                entity.Property(e => e.DatePublish).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
