﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Contracts
{
    public interface IBookingManager
    {
        bool IsRoomAvailable(int room, DateTime date);
        void AddBooking(string guest, int room, DateTime date);
        IEnumerable<int> getAvailableRooms(DateTime date);

    }
}
