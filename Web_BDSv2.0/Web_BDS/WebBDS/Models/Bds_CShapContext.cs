using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebBDS.Models
{
    public partial class Bds_CShapContext : DbContext
    {
        public Bds_CShapContext()
        {
        }

        public Bds_CShapContext(DbContextOptions<Bds_CShapContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ImageProduct> ImageProducts { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Regional> Regionals { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRequest> UserRequests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("BDSConStr"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).HasMaxLength(250);
            });

            modelBuilder.Entity<ImageProduct>(entity =>
            {
                entity.HasKey(e => e.ImgId);

                entity.ToTable("ImageProduct");

                entity.Property(e => e.ImgName).HasMaxLength(250);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ImageProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageProduct_Product");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.DateUp).HasColumnType("date");

                entity.Property(e => e.ImgAvar).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.DateUp).HasColumnType("date");

                entity.Property(e => e.ImgAvar).HasMaxLength(250);

                entity.Property(e => e.LetterPrice).HasMaxLength(50);

                entity.Property(e => e.LinkGgmap).HasColumnName("LinkGGMap");

                entity.Property(e => e.ProductName).HasMaxLength(250);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.Regional)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.RegionalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Regional");
            });

            modelBuilder.Entity<Regional>(entity =>
            {
                entity.ToTable("Regional");

                entity.Property(e => e.RegionalName).HasMaxLength(250);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.FullName).HasMaxLength(250);

                entity.Property(e => e.ImgAvar).HasMaxLength(250);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(250);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<UserRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("UserRequest");

                entity.Property(e => e.DateRequest).HasColumnType("date");

                entity.HasOne(d => d.Use)
                    .WithMany(p => p.UserRequests)
                    .HasForeignKey(d => d.UseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRequest_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
