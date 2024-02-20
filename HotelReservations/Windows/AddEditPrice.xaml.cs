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
    /// Interaction logic for AddEditPrice.xaml
    /// </summary>
    public partial class AddEditPrice : Window
    {
        private PriceListService priceService;

        private Price contextPrice;
        public AddEditPrice(Price? price = null)
        {
            if (price == null)
            {
                contextPrice = new Price();
            }
            else
            {
                contextPrice = price.Clone();
            }

            InitializeComponent();
            priceService = new PriceListService();

            AdjustWindow(price);

            var activeRoomTypes = Hotel.GetInstance().RoomTypes.Where(rt => rt.IsActive);
            RoomTypeCB.ItemsSource = activeRoomTypes;

            ReservationTypeCB.ItemsSource = Enum.GetValues(typeof(ReservationType)).Cast<ReservationType>();

            this.DataContext = contextPrice;
        }
        public void AdjustWindow(Price? price = null)
        {
            if (price != null)
            {
                Title = "Edit Price";
            }
            else
            {
                Title = "Add Price";
            }

        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                if (!IsDuplicatePrice(contextPrice))
                {
                    priceService.SavePrice(contextPrice);
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Cena sa ovim tipom i rezervacijom postoji.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Popunite sva polja.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool IsDuplicatePrice(Price price)
        {
            return priceService.GetAllPrices().Any(p => p.RoomType == price.RoomType && p.ReservationType == price.ReservationType && p.Id != price.Id && p.IsActive);
        }


        private bool Validation()
        {
            if (RoomTypeCB.SelectedItem == null)
            {
                return false;
            }
            if (ReservationTypeCB.SelectedItem == null)
            {
                return false;
            }
            if (!int.TryParse(PriceTextBox.Text, out int priceValue) || priceValue <= 0)
            {
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
