using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jbmg { get; set; }

        public bool IsActive { get; set; } = true;
        public Guest() { }

        public Guest(int id, string name, string surname, string jbmg, bool isActive)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Jbmg = jbmg;
            IsActive = isActive;
        }
        public Guest Clone()
        {
            var clone = new Guest();
            clone.Id = Id;
            clone.Name = Name;
            clone.Surname = Surname;
            clone.Jbmg = Jbmg;
            clone.IsActive = IsActive;
            return clone;
        }
    }
}
