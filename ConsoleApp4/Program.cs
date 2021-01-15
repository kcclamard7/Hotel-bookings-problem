using ConsoleApp4.Contracts;
using ConsoleApp4.Models.Repository;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // create the repo
                IBookingManager bm = new BookingManagerRepository();

                // add booking
                var today = new DateTime(2012, 3, 28);

                Console.WriteLine(await bm.IsRoomAvailable(101, today)
                    .ConfigureAwait(false));

                await bm.AddBooking("Patel", 101, today)
                    .ConfigureAwait(false);

                Console.WriteLine(await bm.IsRoomAvailable(101, today)
                    .ConfigureAwait(false));

                //adding code to display available room
                var rooms = await bm.getAvailableRooms(today)
                    .ConfigureAwait(false);

                Console.WriteLine($"you are lucky! {rooms.Count()} available rooms left for today: {String.Join(", ", rooms.ToArray())}\n");

                await bm.AddBooking("Li", 101, today)
                    .ConfigureAwait(false);

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
