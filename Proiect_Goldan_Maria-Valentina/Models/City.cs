namespace Proiect_Goldan_Maria_Valentina.Models
{
	public class City
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public ICollection<Customer>? Customers { get; set; }
	}
}
