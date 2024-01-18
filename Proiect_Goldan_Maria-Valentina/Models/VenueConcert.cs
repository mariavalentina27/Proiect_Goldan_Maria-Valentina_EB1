using System.Security.Policy;

namespace Proiect_Goldan_Maria_Valentina.Models
{
    public class VenueConcert
    {
        public int VenueID { get; set; }
        public int ConcertID { get; set; }
        public Venue Venue { get; set; }
        public Concert Concert { get; set; }
    }
}
