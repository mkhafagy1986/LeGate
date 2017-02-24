using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Room
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public int Occupancy { get; set; }
        public string Status { get; set; }
    }
}
