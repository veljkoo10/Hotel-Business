using HotelReservations.Model;
using HotelReservations.Service;
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
using System.Windows.Shapes;

namespace HotelReservations.Windows
{
    /// <summary>
    /// Interaction logic for AddEditUser.xaml
    /// </summary>
    public partial class AddEditUser : Window
    {
        private UserService userService;

        private User contextUser;
        public AddEditUser(User? user = null)
        {
            if (user == null)
            {
                contextUser = new User();
            }
            else
            {
                contextUser = user.Clone();
            }

            InitializeComponent(); 
            userService = new UserService();
            AdjustWindow(user);
            this.DataContext = contextUser;
            
        }

        private void AdjustWindow(User user = null)
        {
            UserTypeCB.Items.Add(typeof(Receptionist).Name);
            UserTypeCB.Items.Add(typeof(Administrator).Name);

            if(user != null)
            {
                Title = "Edit user";
                
                UserTypeCB.SelectedItem = user.GetType().Name;
                UserTypeCB.IsEnabled = false;
            }
            else
            {
                Title = "Add user";
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                userService.SaveUsers(contextUser);
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Molimo vas da popunite sva polja pre nego što sačuvate.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool Validation()
        {
            if (string.IsNullOrWhiteSpace(NameTB.Text) ||
                string.IsNullOrWhiteSpace(SurnameTB.Text) ||
                string.IsNullOrWhiteSpace(jmbgTB.Text) ||
                string.IsNullOrWhiteSpace(usernameTB.Text) ||
                string.IsNullOrWhiteSpace(passwordTB.Text) ||
                UserTypeCB.SelectedItem == null)
            {
                MessageBox.Show("Molimo vas da popunite sva polja pre nego što sačuvate.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!long.TryParse(jmbgTB.Text, out _))
            {
                MessageBox.Show("JMBG mora biti celobrojni tip.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }


        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
