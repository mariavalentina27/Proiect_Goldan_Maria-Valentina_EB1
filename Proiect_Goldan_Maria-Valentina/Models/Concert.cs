using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_Goldan_Maria_Valentina.Models
{
	public class Concert
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int ArtistID { get; set; }
		public Artist? Artist { get; set; }

		[Column(TypeName = "decimal(6, 2)")]
		public decimal Price { get; set; }
		public ICollection<Purchase>? Purchases { get; set; }
		public ICollection<VenueConcert>? VenueConcerts { get; set; }
	}
}
