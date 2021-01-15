using ConsoleApp4.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Models.Repository
{
    public class BookingManagerRepository : IBookingManager
    {
        //we initiate the collection of room booked 
        public List<GuestBookedRoom> guestBookedRooms = new List<GuestBookedRoom>();

        // we generate the list of room 
        public List<Room> rooms = new List<Room>
        {
            new Room{RoomId = 101},
            new Room{RoomId = 102},
            new Room{RoomId = 201},
            new Room{RoomId = 203}
        };

        public async Task AddBooking(string guest, int room, DateTime date)
        {
            try
            {
                //checking if room available before adding
                if(await IsRoomAvailable(room, date).ConfigureAwait(false))
                {
                    // create guest
                    var theGuest = new Guest
                    {
                        Name = guest
                    };

                    // book the room
                    var theRoomState = new RoomState
                    {
                        DateBooked = date,
                        TheRoom = new Room { RoomId = room}
                    };

                    // add booked room to the collection
                    await Task.Run(() =>guestBookedRooms.Add(
                        new GuestBookedRoom
                        {
                            TheGuest = theGuest,
                            TheRoomState = theRoomState
                        }));
                }
                else
                {
                    // room not available return error 
                    throw new ArgumentException($"Exeption : Room {room} not available for {date}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<int>> getAvailableRooms(DateTime date)
        {
            try
            {
                // if nothing book then return all the room as available
                if (!guestBookedRooms.Any())
                {
                    return await Task.Run(() => rooms.Select(rm => rm.RoomId));
 
                }
                else
                {
                    // get all the booked room for that date
                    var bookedRoomIds = guestBookedRooms.Where(gbr => gbr.TheRoomState.DateBooked == date)
                        .Select(rm => rm.TheRoomState.TheRoom.RoomId);

                    // return the available room for that date by filtering out all booked room
                    return await Task.Run(() => rooms.Where(rom => !bookedRoomIds.Contains(rom.RoomId))
                       .Select(rm => rm.RoomId));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> IsRoomAvailable(int room, DateTime date)
        {
            
            try
            {
                //check if room collection is not empty then check if a room is available
                if (guestBookedRooms.Any())
                {
                    // if condition matches for argument then room is booked for that date
                    return await Task.Run(() => !guestBookedRooms.Any(br => br.TheRoomState.DateBooked == date && br.TheRoomState.TheRoom.RoomId == room));
                }
                else
                {
                    // if room collection empty assume room is available
                    return await Task.FromResult(true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
