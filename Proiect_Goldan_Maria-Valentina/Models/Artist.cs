using System.ComponentModel.DataAnnotations;

namespace Proiect_Goldan_Maria_Valentina.Models
{
	public class Artist
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public ICollection<Concert>? Concerts { get; set; }
	}
}
