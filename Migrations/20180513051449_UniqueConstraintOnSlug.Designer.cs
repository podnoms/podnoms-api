﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PodNoms.Api.Persistence;

namespace PodNoms.Api.Migrations
{
    [DbContext(typeof(PodNomsDbContext))]
    [Migration("20180513051449_UniqueConstraintOnSlug")]
    partial class UniqueConstraintOnSlug
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rc1-32029")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PodNoms.Api.Models.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("FromUserId");

                    b.Property<string>("Message");

                    b.Property<DateTime?>("MessageSeen");

                    b.Property<Guid>("NewId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("ToUserId");

                    b.Property<string>("Uid");

                    b.Property<DateTime>("UpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("PodNoms.Api.Models.ParsedPlaylistItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool>("IsProcessed");

                    b.Property<Guid>("NewId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<int>("PlaylistId");

                    b.Property<string>("Uid");

                    b.Property<DateTime>("UpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("VideoId");

                    b.Property<string>("VideoType");

                    b.HasKey("Id");

                    b.HasIndex("PlaylistId");

                    b.HasIndex("VideoId", "PlaylistId")
                        .IsUnique()
                        .HasFilter("[VideoId] IS NOT NULL");

                    b.ToTable("ParsedPlaylistItems");
                });

            modelBuilder.Entity("PodNoms.Api.Models.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("NewId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<int>("PodcastId");

                    b.Property<string>("SourceUrl");

                    b.Property<string>("Uid");

                    b.Property<DateTime>("UpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("PodcastId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("PodNoms.Api.Models.Podcast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description");

                    b.Property<Guid>("NewId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("Slug");

                    b.Property<string>("TemporaryImageUrl");

                    b.Property<string>("Title");

                    b.Property<string>("Uid");

                    b.Property<DateTime>("UpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("Podcasts");
                });

            modelBuilder.Entity("PodNoms.Api.Models.PodcastEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AudioFileSize");

                    b.Property<float>("AudioLength");

                    b.Property<string>("AudioUrl");

                    b.Property<string>("Author");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<Guid>("NewId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<int?>("PlaylistId");

                    b.Property<int>("PodcastId");

                    b.Property<bool>("Processed");

                    b.Property<string>("ProcessingPayload");

                    b.Property<int>("ProcessingStatus");

                    b.Property<string>("SourceUrl");

                    b.Property<string>("Title");

                    b.Property<string>("Uid");

                    b.Property<DateTime>("UpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("PlaylistId");

                    b.HasIndex("PodcastId");

                    b.ToTable("PodcastEntries");
                });

            modelBuilder.Entity("PodNoms.Api.Models.ServerConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Key");

                    b.Property<Guid>("NewId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("Uid");

                    b.Property<DateTime>("UpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("ServerConfig","admin");
                });

            modelBuilder.Entity("PodNoms.Api.Services.Auth.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<long?>("FacebookId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PictureUrl");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Slug");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PodNoms.Api.Services.Auth.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PodNoms.Api.Services.Auth.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PodNoms.Api.Services.Auth.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PodNoms.Api.Services.Auth.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PodNoms.Api.Models.ChatMessage", b =>
                {
                    b.HasOne("PodNoms.Api.Services.Auth.ApplicationUser", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId");

                    b.HasOne("PodNoms.Api.Services.Auth.ApplicationUser", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId");
                });

            modelBuilder.Entity("PodNoms.Api.Models.ParsedPlaylistItem", b =>
                {
                    b.HasOne("PodNoms.Api.Models.Playlist", "Playlist")
                        .WithMany("ParsedPlaylistItems")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PodNoms.Api.Models.Playlist", b =>
                {
                    b.HasOne("PodNoms.Api.Models.Podcast", "Podcast")
                        .WithMany()
                        .HasForeignKey("PodcastId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PodNoms.Api.Models.Podcast", b =>
                {
                    b.HasOne("PodNoms.Api.Services.Auth.ApplicationUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("PodNoms.Api.Models.PodcastEntry", b =>
                {
                    b.HasOne("PodNoms.Api.Models.Playlist")
                        .WithMany("PodcastEntries")
                        .HasForeignKey("PlaylistId");

                    b.HasOne("PodNoms.Api.Models.Podcast", "Podcast")
                        .WithMany("PodcastEntries")
                        .HasForeignKey("PodcastId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
