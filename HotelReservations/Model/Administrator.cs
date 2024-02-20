using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    public class Administrator : User
    {
        public Administrator() : base("Administrator")
        {
        }
        public Administrator(string userType) : base(userType)
        {
        }
    }
}
