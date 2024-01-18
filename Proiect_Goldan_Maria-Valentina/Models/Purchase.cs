namespace Proiect_Goldan_Maria_Valentina.Models
{
	public class Purchase
	{
		public int ID { get; set; }
		public int CustomerID { get; set; }
		public int ConcertID { get; set; }
		public DateTime PurchaseDate { get; set; }
		public Customer Customer { get; set; }
		public Concert Concert { get; set; }
	}
}
