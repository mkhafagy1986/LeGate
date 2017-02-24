using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IHotelsProvider
    {
        IEnumerable<Hotel> Hotels { get; set; }
        IEnumerable<Hotel> LoadAllHotels();
    }
}
