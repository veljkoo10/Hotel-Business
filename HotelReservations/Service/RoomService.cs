using HotelReservations.Model;
using HotelReservations.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Service
{
    public class RoomService
    {
        IRoomRepository roomRepository;
        public RoomService() 
        { 
            roomRepository = new RoomRepository();
        }

        public List<Room> GetAllRooms()
        {
            var roomList = new List<Room>();
            foreach(var room in Hotel.GetInstance().Rooms)
            {
                if(room.IsActive)
                {
                    roomList.Add(room);
                }
            }
            return roomList;
        }
        public Room GetRoom(string number)
        {
            foreach(var room in GetAllRooms())
            {
                if(room.RoomNumber==number)
                {
                    return room;
                }

            }
            return null;
        }
        public List<Room> GetSortedRooms()
        {
            var rooms = Hotel.GetInstance().Rooms;
            rooms.Sort((r1, r2) => r1.RoomNumber.CompareTo(r2.RoomNumber));
            return rooms;
        }

        public List<Room> GetAllRoomsByRoomNumber(string startingWith)
        {
            var rooms = Hotel.GetInstance().Rooms;
            var filteredRooms = rooms.FindAll((r) => r.RoomNumber.StartsWith(startingWith));
            return filteredRooms;
        }
        public void DeactivateRoom(string roomNumber)
        {
            var roomtodel = GetRoom(roomNumber);

            if (roomtodel != null)
            {
                    roomtodel.IsActive = false;
            }
        }

        public void SaveRoom(Room room)
        {
            if (room.Id == 0)
            {
                int newRoomId = roomRepository.Insert(room);
                room.Id = newRoomId;
                Hotel.GetInstance().Rooms.Add(room);
            }
            else
            {

                var index = Hotel.GetInstance().Rooms.FindIndex(r => r.Id == room.Id);
                Hotel.GetInstance().Rooms[index] = room;
            }
            roomRepository.Save(Hotel.GetInstance().Rooms);
        }

    }
}
