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

    public partial class AddEditRoom : Window
    {
        private RoomService roomService;

        private Room contextRoom;
        public AddEditRoom(Room? room = null)
        {
            if(room == null)
            {
                contextRoom = new Room();
            }
            else
            {
                contextRoom = room.Clone();
            }
                      
            InitializeComponent();
            roomService = new RoomService();

            AdjustWindow(room);

            this.DataContext = contextRoom;
        }

        public void AdjustWindow(Room? room = null)
        {
            if (room != null)
            {
                Title = "Edit Room";
            }
            else
            {
                Title = "Add Room";
            }

            // OVE PODATKE PREKO SERVISA, PLS
            var activeRoomTypes = Hotel.GetInstance().RoomTypes.Where(rt => rt.IsActive);
            RoomTypesCB.ItemsSource = activeRoomTypes;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                roomService.SaveRoom(contextRoom);

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
            if (string.IsNullOrWhiteSpace(RoomNumberTB.Text))
            {
                MessageBox.Show("Room number cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(RoomNumberTB.Text, out _))
            {
                MessageBox.Show("Room number must be a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(RoomTypesCB.Text))
            {
                MessageBox.Show("Please select a room type.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            var existingRoom = roomService.GetAllRooms().FirstOrDefault(room =>
                room.RoomNumber == contextRoom.RoomNumber &&
                room.RoomType == contextRoom.RoomType);

            if (existingRoom != null)
            {
                MessageBox.Show("Soba sa istim RoomNumber-om i RoomType-om već postoji.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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
