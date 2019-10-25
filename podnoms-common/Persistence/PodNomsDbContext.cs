﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using PodNoms.Data.Extensions;
using PodNoms.Data.Interfaces;
using PodNoms.Data.Models;
using PodNoms.Data.Models.Notifications;

namespace PodNoms.Common.Persistence {
    public static class SeedData {
        public static string AUTH = @"CREATE LOGIN podnomsweb WITH PASSWORD = 'podnomsweb', DEFAULT_DATABASE = [PodNoms], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF
        GO
        ALTER AUTHORIZATION ON DATABASE::[PodNoms] TO[]
        GO";
    }
    public class PodNomsDbContextFactory : IDesignTimeDbContextFactory<PodNomsDbContext> {
        public PodNomsDbContext CreateDbContext(string[] args) {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true)
                .Build();

            Console.WriteLine(configuration);

            var builder = new DbContextOptionsBuilder<PodNomsDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new PodNomsDbContext(builder.Options);
        }
    }


    public sealed class PodNomsDbContext : IdentityDbContext<ApplicationUser> {
        public PodNomsDbContext(DbContextOptions<PodNomsDbContext> options) : base(options) {
            Database.SetCommandTimeout(360);
        }
        // public override void Dispose() {
        //     Console.WriteLine("Should not be here");
        // }
        private IEnumerable<PropertyBuilder> __getColumn(ModelBuilder modelBuilder, string columnName) {
            return modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.Name == columnName)
                .Select(p => modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(p => p.Slug)
                .IsUnique(true);
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.LastSeen)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.IsAdmin)
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder.Entity<Podcast>()
                .HasIndex(p => p.Slug)
                .IsUnique(true);
            modelBuilder.Entity<Podcast>()
                .Property(p => p.AppUserId)
                .IsRequired();
            modelBuilder.Entity<Podcast>()
                .Property(p => p.Private)
                .IsRequired()
                .HasDefaultValue(false);
            modelBuilder.Entity<PodcastEntry>()
                .HasMany(e => e.SharingLinks)
                .WithOne(e => e.PodcastEntry)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PodcastEntry>()
                .Property(p => p.WaveformGenerated)
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder.Entity<PodcastEntrySharingLink>()
                .HasIndex(l => l.LinkIndex)
                .IsUnique();
            modelBuilder.Entity<PodcastEntrySharingLink>()
                .HasIndex(l => l.LinkId)
                .IsUnique();

            modelBuilder.Entity<Playlist>()
                .HasIndex(p => new { p.SourceUrl })
                .IsUnique(true);


            var converter = new EnumToNumberConverter<NotificationOptions, int>();
            modelBuilder.Entity<ApplicationUser>()
                        .Property(e => e.EmailNotificationOptions)
                        .HasConversion(converter);

            foreach (var pb in __getColumn(modelBuilder, "CreateDate")) {
                pb.ValueGeneratedOnAdd()
                    .HasDefaultValueSql("getdate()");
            }

            foreach (var pb in __getColumn(modelBuilder, "UpdateDate")) {
                pb.ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("getdate()");
            }

            // Database.ExecuteSqlCommand (SeedData.CATEGORIES);
            // Database.ExecuteSqlCommand (SeedData.SUB_CATEGORIES);
            // Database.ExecuteSqlCommand (SeedData.AUTH);
        }

        public override int SaveChanges() {
            foreach (var entity in ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                    .Where(e => e.Entity is ISluggedEntity)
                    .Select(e => e.Entity as ISluggedEntity)
                    .Where(e => string.IsNullOrEmpty(e.Slug))) {
                entity.Slug = entity.GenerateSlug(this);
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken)) {
            foreach (var entity in ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                    .Where(e => e.Entity is ISluggedEntity)
                    .Select(e => e.Entity as ISluggedEntity)
                    .Where(e => string.IsNullOrEmpty(e.Slug))) {
                entity.Slug = entity.GenerateSlug(this);
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<AccountSubscription> AccountSubscriptions { get; set; }
        public DbSet<ApplicationUserSlugRedirects> ApplicationUserSlugRedirects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<NotificationLog> NotificationLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PodcastEntry> PodcastEntries { get; set; }
        public DbSet<PodcastEntrySharingLink> PodcastEntrySharingLinks { get; set; }

        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<ServerConfig> ServerConfig { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
    }
}
