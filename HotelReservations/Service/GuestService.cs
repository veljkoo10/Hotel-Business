using HotelReservations.Model;
using HotelReservations.Repository;
using HotelReservations.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HotelReservations.Service
{
    public class GuestService
    {
        private IGuestRepository guestRepository;
        public GuestService()
        {
            guestRepository = new GuestRepository();
        }
        public List<Guest> GetAllGuest()
        {
            return Hotel.GetInstance().Guests.Where(guest => guest.IsActive).ToList();
        }

        public Guest GetGuest(int id)
        {
            return GetAllGuest().FirstOrDefault(guest => guest.Id == id);
        }
        public List<Guest> GetSortedGuests()
        {
            var guests = Hotel.GetInstance().Guests.Where(guest => guest.IsActive).ToList();
            guests.Sort((r1, r2) => r1.Id.CompareTo(r2.Id));
            return guests;
        }
        public void SaveGuests(Guest guest)
        {
            if (!IsDuplicateGuest(guest))
            {
                if (guest.Id == 0)
                {
                    guest.Id = guestRepository.Insert(guest);
                    Hotel.GetInstance().Guests.Add(guest);
                }
                else
                {
                    var index = Hotel.GetInstance().Guests.FindIndex(r => r.Id == guest.Id);
                    Hotel.GetInstance().Guests[index] = guest;
                }
                guestRepository.Save(Hotel.GetInstance().Guests);
            }
            else
            {
                MessageBox.Show("Gost sa ovim podacima već postoji.", "Duplicate Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool IsDuplicateGuest(Guest guest)
        {
            return Hotel.GetInstance().Guests.Any(existingGuest =>
                existingGuest.Name == guest.Name &&
                existingGuest.Surname == guest.Surname &&
                existingGuest.Jbmg == guest.Jbmg &&
                existingGuest.IsActive);
        }


        public void DeleteGuest(int id)
        {
            var guestToDelete = GetGuest(id);
            if (guestToDelete != null)
            {
                guestToDelete.IsActive = false;
                guestRepository.Save(Hotel.GetInstance().Guests);
            }
        }
    }
}
