namespace Proiect_Goldan_Maria_Valentina.Models
{
    public class Venue
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public ICollection<VenueConcert>? VenueConcerts { get; set; }
    }
}
