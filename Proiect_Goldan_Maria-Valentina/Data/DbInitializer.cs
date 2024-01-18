using Microsoft.EntityFrameworkCore;
using Proiect_Goldan_Maria_Valentina.Models;

namespace Proiect_Goldan_Maria_Valentina.Data
{
	public class DbInitializer
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
			{
				if (context.Concerts.Any())
				{
					return; // BD a fost creata anterior 
				}

				Artist Artist1 = new Artist { Name = "Tailor Swift" }; //7
				Artist Artist2 = new Artist { Name = "AgustD" }; //8
				Artist Artist3 = new Artist { Name = "Beyonce" }; //9
				context.Artists.AddRange(Artist1, Artist2, Artist3);
				context.SaveChanges();

				context.Concerts.AddRange(
					new Concert { Name = "Speak Now", Artist = Artist1, Price = Decimal.Parse("220") }, //10
					new Concert { Name = "D-day", Artist = Artist2, Price = Decimal.Parse("180") }, //11
					new Concert { Name = "Renaissance", Artist = Artist3, Price = Decimal.Parse("270") } //12
				);
				context.SaveChanges();

				context.Cities.AddRange(
					new City { Name = "Geneva" }, //4
					new City { Name = "Amsterdam" }, //5
					new City { Name = "Venezuela" } //6
				);
				context.SaveChanges();

				context.Customers.AddRange(
					new Customer { Name = "Popescu Marcela", Adress = "Str. Plopilor, nr. 24", CityID = 4 }, //7
					new Customer { Name = "Mihailescu Cornel", Adress = "Str. Bucuresti, nr. 45, ap. 2", CityID = 5 }, //8
					new Customer { Name = "Vladimirescu Ștefan", Adress = "Str. Pascalopol, nr. 03, ap. 15", CityID = 6 } //9
				);
				context.SaveChanges();

				var purchases = new Purchase[]
				{
					new Purchase{ConcertID=10,CustomerID=7,OrderDate=DateTime.Parse("2023-12-25")},
					new Purchase{ConcertID=11,CustomerID=8,OrderDate=DateTime.Parse("2023-12-26")},
					new Purchase{ConcertID=12,CustomerID=9,OrderDate=DateTime.Parse("2023-12-27")},
					new Purchase{ConcertID=10,CustomerID=9,OrderDate=DateTime.Parse("2023-12-28")},
					new Purchase{ConcertID=11,CustomerID=7,OrderDate=DateTime.Parse("2023-12-29")},
					new Purchase{ConcertID=12,CustomerID=8,OrderDate=DateTime.Parse("2023-12-30")},
				};
				foreach (Purchase p in purchases)
				{
					context.Purchases.Add(p);
				}
				context.SaveChanges();


			}
		}
	}
}
