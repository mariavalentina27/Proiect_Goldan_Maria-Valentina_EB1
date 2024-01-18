namespace Proiect_Goldan_Maria_Valentina.Models
{
	public class Customer
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Adress { get; set; }
		public int CityID { get; set; }
		public City? City { get; set; }
		public ICollection<Purchase> Purchases { get; set; }
	}
}
