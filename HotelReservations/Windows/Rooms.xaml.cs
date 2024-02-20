using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public partial class Rooms : Window
    {
        private ICollectionView view;
        public Rooms()
        {
            InitializeComponent();
            FillData();
            Loaded += RoomType_Loaded;
        }
        private void RoomType_Loaded(object sender, RoutedEventArgs e)
        {
            RoomsDG.SelectedItem = null;
        }
        public void FillData()
        {
            var roomService = new RoomService();
            var rooms = roomService.GetAllRooms();

            view = CollectionViewSource.GetDefaultView(rooms);
            view.Filter = DoFilter;

            RoomsDG.ItemsSource = null;
            RoomsDG.ItemsSource = view;
            RoomsDG.IsSynchronizedWithCurrentItem = true;
        }

        private bool DoFilter(object roomObject)
        {
            var room = roomObject as Room;

            var roomNumberSearchParam = RoomNumberSearchTB.Text;

            if (room.RoomNumber.Contains(roomNumberSearchParam))
            {
                return true;
            }

            return false;
        }

        private void RoomsDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addRoomWindow = new AddEditRoom();

            Hide();
            if (addRoomWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoom = (Room)view.CurrentItem;

            if (selectedRoom != null)
            {
                var editRoomWindow = new AddEditRoom(selectedRoom);

                Hide();

                if (editRoomWindow.ShowDialog() == true)
                {
                    FillData();
                }

                Show();
            }
            else
            {
                MessageBox.Show("Please select a room to edit.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RoomNumberSearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        // TODO: Završi započeto
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoom = view.CurrentItem as Room;

            if (selectedRoom != null)
            {
                var confirmationResult = MessageBox.Show($"Are you sure that you want to delete room {selectedRoom.RoomNumber}?",
                    "Confirmation", MessageBoxButton.YesNo);

                if (confirmationResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        var roomService = new RoomService();
                        roomService.DeactivateRoom(selectedRoom.RoomNumber);
                        roomService.SaveRoom(selectedRoom);

                        FillData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
