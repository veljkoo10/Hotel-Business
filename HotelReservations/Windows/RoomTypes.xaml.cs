using HotelReservations.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for RoomTypes.xaml
    /// </summary>
    public partial class RoomTypes : Window
    {
        private RoomTypeService roomtypeService;
        private ICollectionView view;
        public RoomTypes()
        {
            roomtypeService = new RoomTypeService();
            InitializeComponent();
            FillData();
            Loaded += RoomType_Loaded;
        }
        private void RoomType_Loaded(object sender, RoutedEventArgs e)
        {
            RoomTypeDG.SelectedItem = null;
        }
        private void FillData()
        {
            var roomtype = roomtypeService.GetAllRoomType();

            view = CollectionViewSource.GetDefaultView(roomtype);
            RoomTypeDG.ItemsSource = null;
            RoomTypeDG.ItemsSource = view;
            RoomTypeDG.SelectedItem = null;
            RoomTypeDG.IsSynchronizedWithCurrentItem = true;
        }
        private void AddTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            var addroomtypeWindow = new AddEditRoomType();

            Hide();
            if (addroomtypeWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedType = view.CurrentItem as Model.RoomType;

            if (selectedType != null)
            {
                var editRoomTypeWindow = new AddEditRoomType(selectedType);
                Hide();
                if (editRoomTypeWindow.ShowDialog() == true)
                {
                    FillData();
                }
                Show();
            }
            else
            {
                MessageBox.Show("Please select room type you want to edit.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            var roomtypeService = new Service.RoomTypeService();
            var selectedType = view.CurrentItem as Model.RoomType;

            if (selectedType != null)
            {
                Debug.WriteLine($"Deleting room type: {selectedType.Name}, Id: {selectedType.Id}");
                var result = MessageBox.Show($"Are you sure you want to delete {selectedType.Name}?", "Submit warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (roomtypeService.IsRoomTypeInUse(selectedType))
                    {
                        MessageBox.Show($"Cannot delete {selectedType.Name}. There are active rooms with this room type.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    roomtypeService.DeleteRoomType(selectedType);
                    FillData();
                }
            }
            else
            {
                MessageBox.Show("Please select room type you want to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void RoomNumberSearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                view.Refresh();
            }
            else
            {
                if (int.TryParse(RoomTypeNumberSearchTB.Text, out int roomId))
                {
                    view.Filter = item =>
                    {
                        if (item is Model.RoomType roomType)
                        {
                            return roomType.Id == roomId;
                        }
                        return false;
                    };
                }
                else
                {
                    view.Filter = null;
                }
            }
        }
        private void RoomTypeGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

    }
}
