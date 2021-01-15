using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Models
{
    public class GuestBookedRoom
    {
        public Guest TheGuest { get; set; }
        public RoomState TheRoomState { get; set; }
    }
}
