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
    /// Interaction logic for AddEditRoomType.xaml
    /// </summary>
    public partial class AddEditRoomType : Window
    {
        private RoomTypeService roomTypeService;
        private HotelReservations.Model.RoomType? contextRoomType; 

        public AddEditRoomType(HotelReservations.Model.RoomType? roomType = null)
        {
            if (roomType == null)
            {
                contextRoomType = new HotelReservations.Model.RoomType();
            }
            else
            {
                contextRoomType = roomType.Clone();
            }

            InitializeComponent();
            roomTypeService = new RoomTypeService();
            AdjustWindow(roomType);
            this.DataContext = contextRoomType;
        }

        public void AdjustWindow(HotelReservations.Model.RoomType? roomType = null)
        {
            if (roomType != null)
            {
                Title = "Edit RoomType";
                RoomTypeNameTextBox.Text = roomType.Name;

            }
            else
            {
                Title = "Add RoomType";
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                contextRoomType.Name = RoomTypeNameTextBox.Text;

                roomTypeService.SaveRoomType(contextRoomType!);
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
            return !string.IsNullOrWhiteSpace(RoomTypeNameTextBox.Text);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

}
