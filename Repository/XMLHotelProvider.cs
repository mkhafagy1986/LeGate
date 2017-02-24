using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repository
{
    public class XMLHotelProvider : IHotelsProvider
    {
        private string FileName { get; set; }
        private XDocument HotelsXDocument { get; set; }

        public IEnumerable<Hotel> Hotels { get; set; }

        public XMLHotelProvider(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("FileName cannot be null or empty.", "fileName");

            this.FileName = fileName;
            this.HotelsXDocument = XDocument.Load(FileName);
        }

        public IEnumerable<Hotel> LoadAllHotels()
        {

            return Hotels = from c in HotelsXDocument.Descendants("HOTEL")
                            select new Hotel()
                            {
                                HotelId = (string)c.Attribute("HOTEL_ID"),
                                HotelName = (string)c.Attribute("HOTEL_NAME"),
                                Location = (string)c.Attribute("LOCATION"),
                                Rating = (string)c.Attribute("RATING"),
                                IsAvailable = ((string)c.Attribute("AVAILABLE")) == "Yes" ? true : false,
                                IsRecommendedProduct = ((string)c.Attribute("ISRECOMMENDEDPRODUCT")) == "0" ? true : false,
                                StartingPrice = Decimal.Parse((string)c.Attribute("STARTING_PRICE")),
                                Currency = (string)c.Attribute("CURRENCY"),
                                Rooms = from room in c.Descendants("ROOM")
                                        select new Room()
                                        {
                                            RoomId = (string)room.Element("ROOMID").Value,
                                            RoomName = (string)room.Element("ROOM_NAME").Value,
                                            Occupancy = Int32.Parse((string)room.Element("OCCUPANCY").Value),
                                            Status = (string)room.Element("ROOM_STATUS").Value
                                        }
                            };
        }
    }
}
