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
    /// Interaction logic for AddEditGuest.xaml
    /// </summary>
    public partial class AddEditGuest : Window
    {
        private GuestService guestService;

        private Guest contextGuest;
        public AddEditGuest(Guest? guest = null)
        {
            if (guest == null)
            {
                contextGuest = new Guest();
            }
            else
            {
                contextGuest = guest.Clone();
            }

            InitializeComponent();
            guestService = new GuestService();
            AdjustWindow(guest);
            this.DataContext = contextGuest;

        }

        private void AdjustWindow(Guest guest = null)
        {
            if (guest != null)
            {
                Title = "Edit Guest";

            }
            else
            {
                Title = "Add Guest";
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                contextGuest.Name = NameTB.Text;
                contextGuest.Surname = SurnameTB.Text;
                contextGuest.Jbmg = jmbgTB.Text; 

                guestService.SaveGuests(contextGuest);
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
            if (string.IsNullOrWhiteSpace(NameTB.Text) || string.IsNullOrWhiteSpace(SurnameTB.Text))
            {
                MessageBox.Show("Molimo vas da popunite ime i prezime pre nego što sačuvate.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(jmbgTB.Text) || jmbgTB.Text.Length != 13 || !jmbgTB.Text.All(char.IsDigit))
            {
                MessageBox.Show("JMBG mora biti broj od 13 cifara.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
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
