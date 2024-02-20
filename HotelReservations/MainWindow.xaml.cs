using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelReservations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserService userService;

        public MainWindow()
        {
            InitializeComponent();
            userService = new UserService();
        }

        private void RoomsMI_Click(object sender, RoutedEventArgs e)
        {
            var roomsWindow = new Rooms();
            roomsWindow.Show();
        }

        private void UsersMI_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = new Users();
            usersWindow.Show();
        }
        
        private void RoomTypesMI_Click(object sender, RoutedEventArgs e)
        {
            var roomtypesWindow = new RoomTypes();
            roomtypesWindow.Show();
        }
        private void GuestsMI_Click(object sender, RoutedEventArgs e)
        {
            var guestWindow = new Guests();
            guestWindow.Show();
        }
        private void PricesMI_Click(object sender, RoutedEventArgs e)
        {
            var priceWindow = new Prices();
            priceWindow.Show();
        }
        private void ReservationsMI_Click(object sender, RoutedEventArgs e)
        {
            var reservationWindow = new Reservations();
            reservationWindow.Show();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            User user = userService.GetAllUsers().FirstOrDefault(u => u.Username == username && u.Password == password && u.IsActive);
            if (user != null)
            {
                LoginPanel.Visibility = Visibility.Collapsed;
                AdjustMenuItemsForUserRole(user.UserType);
            }
            else
            {
                MessageBox.Show("Neispravno korisničko ime ili lozinka.", "Greška pri prijavi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //logout 
        private void LogoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ResetMenuItemsVisibility();
            UsernameTextBox.Text = "";
            PasswordBox.Password = "";
            LoginPanel.Visibility = Visibility.Visible;
        }

        private void ResetMenuItemsVisibility()
        {
            RoomsMenuItem.Visibility = Visibility.Collapsed;
            UsersMenuItem.Visibility = Visibility.Collapsed;
            RoomTypesMenuItem.Visibility = Visibility.Collapsed;
            GuestsMenuItem.Visibility = Visibility.Collapsed;
            PricesMenuItem.Visibility = Visibility.Collapsed;
            ReservationMenuItem.Visibility = Visibility.Collapsed;
        }


        private void AdjustMenuItemsForUserRole(string userType)
        {
            ResetMenuItemsVisibility();
            RoomsMenuItem.Visibility = Visibility.Collapsed;
            UsersMenuItem.Visibility = Visibility.Collapsed;
            RoomTypesMenuItem.Visibility = Visibility.Collapsed;
            GuestsMenuItem.Visibility = Visibility.Collapsed;
            PricesMenuItem.Visibility = Visibility.Collapsed;
            ReservationMenuItem.Visibility = Visibility.Collapsed;

            if (userType == "Administrator")
            {
                RoomsMenuItem.Visibility = Visibility.Visible;
                UsersMenuItem.Visibility = Visibility.Visible;
                PricesMenuItem.Visibility = Visibility.Visible;
                GuestsMenuItem.Visibility = Visibility.Visible;
                RoomTypesMenuItem.Visibility = Visibility.Visible;
            }
            else if (userType == "Receptionist")
            {
                GuestsMenuItem.Visibility = Visibility.Visible;
                ReservationMenuItem.Visibility = Visibility.Visible;
            }

            MainMenu.Visibility = Visibility.Visible;
        }



    }
}
