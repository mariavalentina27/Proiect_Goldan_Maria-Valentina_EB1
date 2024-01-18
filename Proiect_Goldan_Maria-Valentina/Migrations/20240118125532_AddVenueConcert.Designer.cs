﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proiect_Goldan_Maria_Valentina.Data;

#nullable disable

namespace Proiect_Goldan_Maria_Valentina.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20240118125532_AddVenueConcert")]
    partial class AddVenueConcert
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Artist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Artist", (string)null);
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("City", (string)null);
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Concert", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ArtistID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(6, 2)");

                    b.HasKey("ID");

                    b.HasIndex("ArtistID");

                    b.ToTable("Concert", (string)null);
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CityID");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Purchase", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ConcertID")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ConcertID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Purchase", (string)null);
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Venue", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Venue", (string)null);
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.VenueConcert", b =>
                {
                    b.Property<int>("ConcertID")
                        .HasColumnType("int");

                    b.Property<int>("VenueID")
                        .HasColumnType("int");

                    b.HasKey("ConcertID", "VenueID");

                    b.HasIndex("VenueID");

                    b.ToTable("VenueConcert", (string)null);
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Concert", b =>
                {
                    b.HasOne("Proiect_Goldan_Maria_Valentina.Models.Artist", "Artist")
                        .WithMany("Concerts")
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Customer", b =>
                {
                    b.HasOne("Proiect_Goldan_Maria_Valentina.Models.City", "City")
                        .WithMany("Customers")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Purchase", b =>
                {
                    b.HasOne("Proiect_Goldan_Maria_Valentina.Models.Concert", "Concert")
                        .WithMany("Purchases")
                        .HasForeignKey("ConcertID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proiect_Goldan_Maria_Valentina.Models.Customer", "Customer")
                        .WithMany("Purchases")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Concert");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.VenueConcert", b =>
                {
                    b.HasOne("Proiect_Goldan_Maria_Valentina.Models.Concert", "Concert")
                        .WithMany("VenueConcerts")
                        .HasForeignKey("ConcertID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proiect_Goldan_Maria_Valentina.Models.Venue", "Venue")
                        .WithMany("VenueConcerts")
                        .HasForeignKey("VenueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Concert");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Artist", b =>
                {
                    b.Navigation("Concerts");
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.City", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Concert", b =>
                {
                    b.Navigation("Purchases");

                    b.Navigation("VenueConcerts");
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Customer", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Venue", b =>
                {
                    b.Navigation("VenueConcerts");
                });
#pragma warning restore 612, 618
        }
    }
}