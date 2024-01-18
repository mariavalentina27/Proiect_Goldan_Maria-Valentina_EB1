using System.ComponentModel.DataAnnotations;

namespace Proiect_Goldan_Maria_Valentina.Models.LibraryViewModels
{
    public class PurchaseGroup
    {
        [DataType(DataType.Date)]
        public DateTime? PurchaseDate { get; set; }
        public int ConcertCount { get; set; }
    }
}
