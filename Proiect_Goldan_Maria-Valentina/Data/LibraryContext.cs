﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Proiect_Goldan_Maria_Valentina.Models;

namespace Proiect_Goldan_Maria_Valentina.Data
{
	public class LibraryContext : DbContext
	{
		public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
		{ 
		}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Purchase> Purchases { get; set; }
		public DbSet<Concert> Concerts { get; set; }
		public DbSet<Artist> Artists { get; set; }
		public DbSet<City> Cities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) 
		{ 
			modelBuilder.Entity<Customer>().ToTable("Customer"); 
			modelBuilder.Entity<Purchase>().ToTable("Purchase"); 
			modelBuilder.Entity<Concert>().ToTable("Concert"); 
			modelBuilder.Entity<Artist>().ToTable("Artist"); 
			modelBuilder.Entity<City>().ToTable("City"); 
		}
	}
}
