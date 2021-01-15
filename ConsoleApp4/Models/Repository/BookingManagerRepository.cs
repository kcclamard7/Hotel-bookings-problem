﻿using ConsoleApp4.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp4.Models.Repository
{
    public class BookingManagerRepository : IBookingManager
    {
        //we initiate the collection of room booked by guess
        public List<GuestBookedRoom> guestBookedRooms = new List<GuestBookedRoom>();

        // we generate the list of room 
        public List<Room> rooms = new List<Room>
        {
            new Room{RoomId = 101},
            new Room{RoomId = 102},
            new Room{RoomId = 201},
            new Room{RoomId = 203}
        };

        public void AddBooking(string guest, int room, DateTime date)
        {
            try
            {
                //checking if room available before adding
                if(IsRoomAvailable(room, date))
                {
                    // create guest
                    var theGuest = new Guest
                    {
                        Name = guest
                    };

                    // booked the room
                    var theRoomState = new RoomState
                    {
                        DateBooked = date,
                        TheRoom = new Room { RoomId = room}
                    };

                    // add booked room to the list
                    guestBookedRooms.Add(
                        new GuestBookedRoom
                        {
                            TheGuest = theGuest,
                            TheRoomState = theRoomState
                        });
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

        public IEnumerable<int> getAvailableRooms(DateTime date)
        {
            try
            {
                if (!guestBookedRooms.Any())
                {
                    return rooms.Select(rm => rm.RoomId);
                }
                else
                {
                    // get all the booked room for that date
                    var bookedRoomIds = guestBookedRooms.Where(gbr => gbr.TheRoomState.DateBooked == date)
                        .Select(rm => rm.TheRoomState.TheRoom.RoomId);

                    // return the available room for that date 
                    var roomAvailables = rooms.Where(rom => !bookedRoomIds.Contains(rom.RoomId))
                       .Select(rm => rm.RoomId);

                    return roomAvailables.Any() ? roomAvailables : new List<int> { };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsRoomAvailable(int room, DateTime date)
        {
            
            try
            {
                //check if room collection is not empty then check if the room is available
                if (guestBookedRooms.Any())
                {
                    // if condition matches for argument then room is booked for that date
                    return !guestBookedRooms.Any(br => br.TheRoomState.DateBooked == date && br.TheRoomState.TheRoom.RoomId == room);
                }
                else
                {
                    // if room collection empty assume room is available
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}