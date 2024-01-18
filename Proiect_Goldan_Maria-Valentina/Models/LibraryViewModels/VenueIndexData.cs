using System.Security.Policy;

namespace Proiect_Goldan_Maria_Valentina.Models.LibraryViewModels
{
    public class VenueIndexData
    {
        public IEnumerable<Venue> Venues { get; set; }
        public IEnumerable<Concert> Concerts { get; set; }
        public IEnumerable<Purchase> Purchases { get; set; }
    }
}
