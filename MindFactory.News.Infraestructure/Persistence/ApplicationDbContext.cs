// <copyright file="ApplicationDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Infraestructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using MindFactory.News.Application.Interfaces;
    using MindFactory.News.Domain.Entities;

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => base.SaveChangesAsync(cancellationToken);

        public virtual DbSet<NewsItem> NewsItems { get; set; }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Editorial> Editorials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("unaccent");
            modelBuilder.HasPostgresExtension("pg_trgm");

            modelBuilder.HasDefaultSchema("admin");

            modelBuilder.Entity<NewsItem>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_NewsItem");

                entity.ToTable("NewsItems", "news");

                entity.Property(e => e.CreatedDateTime)
                    .HasDefaultValueSql("(now())::timestamp without time zone")
                    .HasColumnType("timestamp without time zone");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("timestamp without time zone")
                    .IsRequired(false);

                entity.HasOne(e => e.Author)
                    .WithMany(a => a.NewsItems)
                    .HasForeignKey(e => e.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_NewsItem_AuthorId");

                entity.HasOne(e => e.Editorial)
                    .WithMany(ed => ed.NewsItems)
                    .HasForeignKey(e => e.EditorialId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_NewsItem_EditorialId");

                entity.Property(typeof(NpgsqlTypes.NpgsqlTsVector), "SearchVector")
                    .HasColumnType("tsvector");

                entity.HasIndex("SearchVector")
                    .HasMethod("GIN");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_Author");

                entity.ToTable("Authors", "news");

                entity.Property(e => e.CreatedDateTime)
                    .HasDefaultValueSql("(now())::timestamp without time zone")
                    .HasColumnType("timestamp without time zone");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("timestamp without time zone")
                    .IsRequired(false);
            });
            
            modelBuilder.Entity<Editorial>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_Editorial");

                entity.ToTable("Editorials", "news");

                entity.Property(e => e.CreatedDateTime)
                    .HasDefaultValueSql("(now())::timestamp without time zone")
                    .HasColumnType("timestamp without time zone");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("timestamp without time zone")
                    .IsRequired(false);
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}