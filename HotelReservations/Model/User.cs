using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string JMBG { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private string userType;
        public string UserType
        {
            get { return userType ?? GetType().Name; }
            set { userType = value; }
        }
        public bool IsActive { get; set; } = true;
        public User() {

        }


        public User(string userType)
        {
            UserType = userType;
        }
        public User Clone()
        {
            var clone = new User();
            clone.Id = Id;
            clone.Name = Name;
            clone.Surname = Surname;
            clone.JMBG = JMBG;
            clone.Username = Username;
            clone.Password = Password;
            clone.UserType = UserType;
            clone.IsActive = IsActive;
            return clone;
        }
    }

}
