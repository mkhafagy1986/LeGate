using Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
//using System.Web.

namespace HotelsViewer.Controllers
{
    public class HotelsController : Controller
    {
        HttpClient client;
        public HotelsController()
        {
            client = new HttpClient();
        }
        // GET: Hotels
        public ActionResult Index()
        {
            return View(SearchHotels("HotelName", "", true));
        }

        public ActionResult Filter(string SearchValue, string SearchCriteria, bool IsReady)
        {
            return PartialView("HotelsView", SearchHotels(SearchCriteria, SearchValue, IsReady));
        }

        List<Hotel> GetAllHotels()
        {
            List<Hotel> AllHotelsList = null;

            client.BaseAddress = new Uri(Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.IndexOf(Request.Url.PathAndQuery)));
            //Call HttpClient.GetAsync to send a GET request to the appropriate URI   
            HttpResponseMessage resp = client.GetAsync("api/HotelsApi/GetAllHotels").Result;
            //This method throws an exception if the HTTP response status is an error code.  
            resp.EnsureSuccessStatusCode();
            AllHotelsList = (List<Hotel>)resp.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;

            return AllHotelsList;
        }

        List<Hotel> SearchHotels(string SearchFieldName, string SerachFieldValue, bool IsReady)
        {
            List<Hotel> AllHotelsList = null;
            try
            {
                client.BaseAddress = new Uri(Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.IndexOf(Request.Url.PathAndQuery)));
            }
            catch
            {
                client.BaseAddress = new Uri(Request.Url.AbsoluteUri);
            }
            //client.BaseAddress = new Uri(Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.IndexOf(Request.Url.PathAndQuery)));
            //Call HttpClient.GetAsync to send a GET request to the appropriate URI   
            HttpResponseMessage resp = client.GetAsync("api/HotelsApi/Search/" + SearchFieldName + "/" + (string.IsNullOrEmpty(SerachFieldValue) == true ? "_" : SerachFieldValue) + "/" + IsReady.ToString()).Result;
            //This method throws an exception if the HTTP response status is an error code.  
            resp.EnsureSuccessStatusCode();
            AllHotelsList = (List<Hotel>)resp.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;

            return AllHotelsList;
        }
    }
}