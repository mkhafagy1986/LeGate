using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Hotel
    {
        public string HotelId { get; set; }
        public string HotelName { get; set; }
        public string Location { get; set; }
        public string Rating { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsRecommendedProduct { get; set; }
        public decimal StartingPrice { get; set; }
        public string Currency { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}
