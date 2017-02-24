using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace HotelsViewer.Controllers
{
    public class HotelsApiController : ApiController
    {
        public string XMLFileName { get; set; }
        public string JSONFileName { get; set; }
        public HotelsApiController()
        {
            XMLFileName = ConfigurationManager.AppSettings["HotelsXml"];
            JSONFileName = ConfigurationManager.AppSettings["HotelsJson"];
        }

        private string GetAbsolutPath(string FilePath)
        {
            return System.Web.Hosting.HostingEnvironment.MapPath(FilePath);
        }

        public IEnumerable<Hotel> LoadXMLHotels()
        {
            IHotelsProvider HotelsFromXml = new XMLHotelProvider(GetAbsolutPath(XMLFileName));
            IEnumerable<Hotel> HotelsList = HotelsFromXml.LoadAllHotels();
            return HotelsList;
        }
        public IEnumerable<Hotel> LoadJSONHotels()
        {
            IHotelsProvider HotelsFromJson = new JSONHotelProvider(GetAbsolutPath(JSONFileName));
            IEnumerable<Hotel> HotelsList = HotelsFromJson.LoadAllHotels();
            return HotelsList;
        }

        // GET: api/Hotels
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        //[Route(Name = "Hotels/GetAllHotels")]
        public List<Hotel> GetAllHotels()
        {
            return GetUnifiedHotelsList();
        }

        private List<Hotel> GetUnifiedHotelsList()
        {
            List<Hotel> AllHotels = new List<Hotel>();
            IEnumerable<Hotel> XmlHotelsList = LoadXMLHotels();
            IEnumerable<Hotel> JsonHotelsList = LoadJSONHotels();
            AllHotels.AddRange(XmlHotelsList);
            AllHotels.AddRange(JsonHotelsList);
            return AllHotels;
        }

        // GET: api/Hotels/5
        [System.Web.Http.AcceptVerbs("Get", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/HotelsApi/Search/{SearchFieldName?}/{SerachFieldValue?}/{IsReady?}")]
        public List<Hotel> Search(string SearchFieldName, string SerachFieldValue, string IsReady)
        {
            return SearchForHotel(SearchFieldName, SerachFieldValue, IsReady);
        }

        // GET: api/Hotels/5
        [System.Web.Http.AcceptVerbs("Get", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/HotelsApi/Search/{SearchFieldName?}/{SerachFieldValue?}")]
        public List<Hotel> Search(string SearchFieldName, string SerachFieldValue)
        {
            return SearchForHotel(SearchFieldName, SerachFieldValue, "false");
        }

        private List<Hotel> SearchForHotel(string SearchFieldName, string SearchFieldValue, string IsReady)
        {
            bool _IsReady = bool.Parse(IsReady);
            List<Hotel> AllHotels = GetUnifiedHotelsList();
            List<Hotel> SearchResult = new List<Hotel>();

            if (string.IsNullOrEmpty(SearchFieldValue) || SearchFieldValue == "_")
                return AllHotels.Where(hotel => hotel.IsAvailable == _IsReady).ToList();

            switch (SearchFieldName)
            {
                case "HotelName":
                    {
                        SearchResult = AllHotels.Where(hotel => hotel.HotelName.ToLower().Contains(SearchFieldValue.ToLower()) && hotel.IsAvailable == _IsReady).ToList();
                        break;
                    }
                case "HotelRating":
                    {
                            SearchResult = AllHotels.Where(hotel => hotel.Rating.ToLower().Contains(SearchFieldValue.ToLower()) && hotel.IsAvailable == _IsReady).ToList();
                        break;
                    }
                case "HotelId":
                    {
                        SearchResult = AllHotels.Where(hotel => hotel.HotelId == SearchFieldValue).ToList();
                        break;
                    }
            }
            return SearchResult;
        }
    }
}
