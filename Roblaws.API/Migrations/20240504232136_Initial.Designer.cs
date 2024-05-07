﻿// <auto-generated />
using System;
using JWTAuthTemplate.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JWTAuthTemplate.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240504232136_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationUserToken", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbComparisonPrice", b =>
                {
                    b.Property<int>("ComparisonPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ComparisonPriceId"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ExpiryDate")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<bool>("IsComparisonPrice")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMemberOnly")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMemberOnlyComparisonPrice")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastChecked")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OfferId")
                        .HasColumnType("integer");

                    b.Property<double?>("Quantity")
                        .HasColumnType("double precision");

                    b.Property<int?>("ReasonCode")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Unit")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<double?>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("ComparisonPriceId");

                    b.HasIndex("OfferId");

                    b.ToTable("ComparisonPrices");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbMemberOnlyComparisonPrice", b =>
                {
                    b.Property<int>("MemberComparisonPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MemberComparisonPriceId"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ExpiryDate")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<bool>("IsComparisonPrice")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMemberOnly")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMemberOnlyComparisonPrice")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastChecked")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OfferId")
                        .HasColumnType("integer");

                    b.Property<double?>("Quantity")
                        .HasColumnType("double precision");

                    b.Property<int?>("ReasonCode")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Unit")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<double?>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("MemberComparisonPriceId");

                    b.HasIndex("OfferId");

                    b.ToTable("MemberOnlyComparisonPrices");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbMemberOnlyPrimaryPrice", b =>
                {
                    b.Property<int>("MemberOnlyPrimaryPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MemberOnlyPrimaryPriceId"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ExpiryDate")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<bool>("IsComparisonPrice")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMemberOnly")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMemberOnlyComparisonPrice")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastChecked")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OfferId")
                        .HasColumnType("integer");

                    b.Property<double?>("Quantity")
                        .HasColumnType("double precision");

                    b.Property<int?>("ReasonCode")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Unit")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<double?>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("MemberOnlyPrimaryPriceId");

                    b.ToTable("MemberOnlyPrimaryPrices");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbOffer", b =>
                {
                    b.Property<int>("OfferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OfferId"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DbMemberOnlyPrimaryPriceId")
                        .HasColumnType("integer");

                    b.Property<int>("DbPrimaryPriceId")
                        .HasColumnType("integer");

                    b.Property<int>("DbProductProductId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastChecked")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OfferType")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("SellerId")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("StockStatus")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.HasKey("OfferId");

                    b.HasIndex("DbMemberOnlyPrimaryPriceId")
                        .IsUnique();

                    b.HasIndex("DbPrimaryPriceId")
                        .IsUnique();

                    b.HasIndex("DbProductProductId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbPrimaryPrice", b =>
                {
                    b.Property<int>("PrimaryPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PrimaryPriceId"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ExpiryDate")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<bool>("IsComparisonPrice")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMemberOnly")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMemberOnlyComparisonPrice")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastChecked")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OfferId")
                        .HasColumnType("integer");

                    b.Property<double?>("Quantity")
                        .HasColumnType("double precision");

                    b.Property<int?>("ReasonCode")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Unit")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<double?>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("PrimaryPriceId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<string>("ArticleNumber")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Brand")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<string>("Ingredients")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<bool>("IsPrimaryVariant")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVariant")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastChecked")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Link")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("MchCode")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("PackageSize")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("StoreId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Uom")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationRoleClaim", b =>
                {
                    b.HasOne("JWTAuthTemplate.Models.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationUserClaim", b =>
                {
                    b.HasOne("JWTAuthTemplate.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationUserLogin", b =>
                {
                    b.HasOne("JWTAuthTemplate.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationUserRole", b =>
                {
                    b.HasOne("JWTAuthTemplate.Models.Identity.ApplicationRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JWTAuthTemplate.Models.Identity.ApplicationUser", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationUserToken", b =>
                {
                    b.HasOne("JWTAuthTemplate.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbComparisonPrice", b =>
                {
                    b.HasOne("JWTAuthTemplate.Models.Loblaws.DbOffer", "Offer")
                        .WithMany("ComparisonPrices")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbMemberOnlyComparisonPrice", b =>
                {
                    b.HasOne("JWTAuthTemplate.Models.Loblaws.DbOffer", "Offer")
                        .WithMany("MemberOnlyComparisonPrices")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbOffer", b =>
                {
                    b.HasOne("JWTAuthTemplate.Models.Loblaws.DbMemberOnlyPrimaryPrice", "MemberOnlyPrimaryPrice")
                        .WithOne("Offer")
                        .HasForeignKey("JWTAuthTemplate.Models.Loblaws.DbOffer", "DbMemberOnlyPrimaryPriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JWTAuthTemplate.Models.Loblaws.DbPrimaryPrice", "PrimaryPrice")
                        .WithOne("Offer")
                        .HasForeignKey("JWTAuthTemplate.Models.Loblaws.DbOffer", "DbPrimaryPriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JWTAuthTemplate.Models.Loblaws.DbProduct", "Product")
                        .WithMany("Offers")
                        .HasForeignKey("DbProductProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MemberOnlyPrimaryPrice");

                    b.Navigation("PrimaryPrice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Identity.ApplicationUser", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbMemberOnlyPrimaryPrice", b =>
                {
                    b.Navigation("Offer")
                        .IsRequired();
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbOffer", b =>
                {
                    b.Navigation("ComparisonPrices");

                    b.Navigation("MemberOnlyComparisonPrices");
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbPrimaryPrice", b =>
                {
                    b.Navigation("Offer")
                        .IsRequired();
                });

            modelBuilder.Entity("JWTAuthTemplate.Models.Loblaws.DbProduct", b =>
                {
                    b.Navigation("Offers");
                });
#pragma warning restore 612, 618
        }
    }
}
