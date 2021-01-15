using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Contracts
{
    public interface IBookingManager
    {
        Task<bool> IsRoomAvailable(int room, DateTime date);
        Task AddBooking(string guest, int room, DateTime date);
        Task<IEnumerable<int>> getAvailableRooms(DateTime date);

    }
}
