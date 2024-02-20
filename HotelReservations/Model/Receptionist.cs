using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    public class Receptionist : User
    {
        // Dodatni konstruktor za Recepcionera
        public Receptionist() : base("Receptionist")
        {
        }

        // Dodatni konstruktor koji zadovoljava zahtev trenutnog konstruktora u User klasi
        public Receptionist(string userType) : base(userType)
        {
        }
    }
}
