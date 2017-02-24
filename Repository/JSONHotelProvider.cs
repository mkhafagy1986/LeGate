using Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class JSONHotelProvider : IHotelsProvider
    {
        private string FileName { get; set; }
        private StreamReader JSFileReader { get; set; }
        private JObject HotelsJson { get; set; }
        public IEnumerable<Hotel> Hotels { get; set; }
        public JSONHotelProvider(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("FileName cannot be null or empty.", "fileName");

            this.FileName = fileName;
            this.JSFileReader = File.OpenText(FileName);
        }

        public IEnumerable<Hotel> LoadAllHotels()
        {
            string json = JSFileReader.ReadToEnd();
            this.HotelsJson = JObject.Parse(json);

            return Hotels = from c in HotelsJson["avaliabilitiesResponse"]["Hotels"]["Hotel"]
                            select new Hotel()
                            {
                                HotelId = (string)c["HotelCode"],
                                HotelName = (string)c["HotelsName"],
                                Location = (string)c["Location"],
                                Rating = (string)c["Rating"],
                                IsAvailable = ((string)c["IsReady"]) == "true" ? true : false,
                                StartingPrice = Decimal.Parse((string)c["LowestPrice"]),
                                Currency = (string)c["Currency"],
                                Rooms = from room in c["AvailableRooms"]["AvailableRoom"]
                                        select new Room()
                                        {
                                            RoomId = (string)room["RoomCode"],
                                            RoomName = (string)room["RoomName"],
                                            Occupancy = Int32.Parse((string)room["Occupancy"]),
                                            Status = (string)room["Status"]
                                        }
                            };
        }

    }
}
