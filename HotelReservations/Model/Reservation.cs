using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public ReservationType ReservationType { get; set; }

        public List<Guest> Guests { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsActive { get; set; } = true;

        public Reservation()
        {

        }

        public Reservation(int id, string roomNumber, ReservationType reservationType, List<Guest> guests, DateTime startDateTime, DateTime endDateTime, decimal totalPrice, bool isActive)
        {
            Id = id;
            RoomNumber = roomNumber;
            ReservationType = reservationType;
            Guests = guests;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            TotalPrice = totalPrice;
            IsActive = isActive;
        }

        public Reservation Clone()
        {
            return new Reservation
            {
                Id = this.Id,
                RoomNumber = this.RoomNumber,
                ReservationType = this.ReservationType,
                Guests = this.Guests != null ? new List<Guest>(this.Guests) : new List<Guest>(),
                StartDateTime = this.StartDateTime,
                EndDateTime = this.EndDateTime,
                TotalPrice = this.TotalPrice,
                IsActive = this.IsActive
            };
        }

    }
}
