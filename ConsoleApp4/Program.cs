using ConsoleApp4.Contracts;
using ConsoleApp4.Models.Repository;

using System;
using System.Linq;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // create the repo
                IBookingManager bm = new BookingManagerRepository();

                // add booking
                var today = new DateTime(2012, 3, 28);

                Console.WriteLine(bm.IsRoomAvailable(101, today));

                bm.AddBooking("Patel", 101, today);

                Console.WriteLine(bm.IsRoomAvailable(101, today));

                //adding code to display available room
                var rooms = bm.getAvailableRooms(today);
                Console.WriteLine($"you are lucky! {rooms.Count()} available rooms left for today: {String.Join(", ", rooms.ToArray())}\n");

                bm.AddBooking("Li", 101, today);

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
