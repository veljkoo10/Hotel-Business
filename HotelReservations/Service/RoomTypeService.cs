using HotelReservations.Model;
using HotelReservations.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservations.Service
{
    public class RoomTypeService
    {
        private IRoomTypeRepository roomtypeRepository;

        public RoomTypeService()
        {
            roomtypeRepository = new RoomTypeRepository();
        }

        public List<RoomType> GetAllRoomType()
        {
            return Hotel.GetInstance().RoomTypes.Where(roomtype => roomtype.IsActive).ToList();
        }

        public RoomType GetRoomType(int id)
        {
            return GetAllRoomType().FirstOrDefault(roomtype => roomtype.Id == id);
        }

        public List<RoomType> GetSortedUsers()
        {
            var roomtypes = Hotel.GetInstance().RoomTypes.Where(roomtypes => roomtypes.IsActive).ToList();
            roomtypes.Sort((r1, r2) => r1.Id.CompareTo(r2.Id));
            return roomtypes;
        }

        public void SaveRoomType(RoomType roomtype)
        {
            if (IsRoomTypeUnique(roomtype))
            {
                if (roomtype.Id == 0)
                {
                    roomtype.Id = roomtypeRepository.Insert(roomtype);
                    Hotel.GetInstance().RoomTypes.Add(roomtype);
                }
                else
                {
                    roomtypeRepository.Update(roomtype);
                    var index = Hotel.GetInstance().RoomTypes.FindIndex(r => r.Id == roomtype.Id);
                    Hotel.GetInstance().RoomTypes[index] = roomtype;
                }
                roomtypeRepository.Save(Hotel.GetInstance().RoomTypes);
            }
            else
            {
                MessageBox.Show("Room type with the same name already exists.", "Duplicate Name", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool IsRoomTypeUnique(RoomType roomtype)
        {
            return !Hotel.GetInstance().RoomTypes.Any(r => r.Id != roomtype.Id && r.Name.ToLower() == roomtype.Name.ToLower() && r.IsActive);
        }

        public bool IsRoomTypeInUse(RoomType roomType)
        {
            var roomService = new RoomService();
            var roomsWithRoomType = roomService.GetAllRooms().Any(room => room.RoomType == roomType && room.IsActive);
            return roomsWithRoomType;
        }

        public void DeleteRoomType(RoomType roomType)
        {
            if (IsRoomTypeInUse(roomType))
            {
                MessageBox.Show($"Cannot delete {roomType.Name}. There are active rooms with this room type.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var roomToDelete = GetRoomType(roomType.Id);

            if (roomToDelete != null)
            {
                Debug.WriteLine($"Before change: Room {roomToDelete.Id}, IsActive: {roomToDelete.IsActive}");

                roomToDelete.IsActive = false;
                Debug.WriteLine($"After change: Room {roomToDelete.Id}, IsActive: {roomToDelete.IsActive}");
                roomtypeRepository.Save(Hotel.GetInstance().RoomTypes);
            }
        }

    }
}
