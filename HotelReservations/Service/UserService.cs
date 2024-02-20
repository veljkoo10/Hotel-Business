using HotelReservations.Model;
using HotelReservations.Repository;
using HotelReservations.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HotelReservations.Service
{
    public class UserService
    {
        private IUsersRepository userRepository;

        public UserService()
        {
            userRepository = new UsersRepository();
        }

        public List<User> GetAllUsers()
        {
            return userRepository.LoadAll().Where(user => user.IsActive).ToList();
        }


        public User GetUser(int id)
        {
            return GetAllUsers().FirstOrDefault(user => user.Id == id);
        }

        public List<User> GetSortedUsers()
        {
            var users = Hotel.GetInstance().Users.Where(user => user.IsActive).ToList();
            users.Sort((r1, r2) => r1.Id.CompareTo(r2.Id));
            return users;
        }

        public void SaveUsers(User user)
        {
            if (!IsDuplicateUser(user))
            {
                if (user.Id == 0)
                {
                    user.Id = userRepository.Insert(user);
                    Hotel.GetInstance().Users.Add(user);
                }
                else
                {
                    var index = Hotel.GetInstance().Users.FindIndex(r => r.Id == user.Id);
                    Hotel.GetInstance().Users[index] = user;
                }
                userRepository.Save(Hotel.GetInstance().Users);
            }
            else
            {
                MessageBox.Show("Osoba sa ovim podacima.", "Duplicate Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool IsDuplicateUser(User user)
        {
            return Hotel.GetInstance().Users.Any(existingUser =>
                existingUser.Name == user.Name &&
                existingUser.Surname == user.Surname &&
                existingUser.JMBG == user.JMBG &&
                existingUser.Username == user.Username &&
                existingUser.Password == user.Password &&
                existingUser.IsActive);
        }


        public void DeleteUser(int id)
        {
            var userToDelete = GetUser(id);
            if (userToDelete != null)
            {
                userToDelete.IsActive = false;
                userRepository.Update(userToDelete); 
            }
        }


    }
}
