using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Repository
{
    public interface IUsersRepository
    {
        List<User> LoadAll();
        int Insert(User user);
        void Update(User user);
        void Save(List<User> userList);
    }

}
