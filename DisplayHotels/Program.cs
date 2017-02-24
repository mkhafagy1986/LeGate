using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayHotels
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlFileName = AppDomain.CurrentDomain.BaseDirectory + ConfigurationSettings.AppSettings["HotelsXml"];
            string csvFileName = AppDomain.CurrentDomain.BaseDirectory + ConfigurationSettings.AppSettings["HotelsJson"];

            IHotelsProvider HotelsFromXml = new XMLHotelProvider(xmlFileName);
            OutputHotels(HotelsFromXml);

            Console.WriteLine("");
            IHotelsProvider HotelsFromJSON = new JSONHotelProvider(csvFileName);
            OutputHotels(HotelsFromJSON);

        }

        static void OutputHotels(IHotelsProvider HotelProvider)
        {
            Console.WriteLine("CustomerLeads:");
            //foreach (UnifiedHotel hotel in HotelProvider.LoadAllHotels())
            //{
            //    Console.WriteLine("{0} {1} - {2}", hotel.HotelId, hotel.HotelName, hotel.Location);
            //}
        }
    }
}
