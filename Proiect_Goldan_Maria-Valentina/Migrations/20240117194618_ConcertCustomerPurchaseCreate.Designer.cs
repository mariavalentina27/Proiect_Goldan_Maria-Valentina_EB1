﻿// <auto-generated />
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
    [Migration("20240117194618_ConcertCustomerPurchaseCreate")]
    partial class ConcertCustomerPurchaseCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Concert", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Purchase", b =>
                {
                    b.Property<int>("PurchaseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseID"));

                    b.Property<int>("ConcertID")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.HasKey("PurchaseID");

                    b.HasIndex("ConcertID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Purchase", (string)null);
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

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Concert", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("Proiect_Goldan_Maria_Valentina.Models.Customer", b =>
                {
                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}
