using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelReservations.Windows
{
    /// <summary>
    /// Interaction logic for Guests.xaml
    /// </summary>
    public partial class Guests : Window
    {
        private GuestService guestService;
        private ICollectionView view;
        public Guests()
        {
            guestService = new GuestService();

            InitializeComponent();
            FillData();
            Loaded += Guests_Loaded;
        }

        private void Guests_Loaded(object sender, RoutedEventArgs e)
        {
            GuestsDG.SelectedItem = null;
        }

        private void FillData()
        {
            var users = guestService.GetAllGuest();
            view = CollectionViewSource.GetDefaultView(users);
            GuestsDG.ItemsSource = null; 
            GuestsDG.ItemsSource = view;
            GuestsDG.IsSynchronizedWithCurrentItem = true;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addGuestWindow = new AddEditGuest();

            Hide();
            if (addGuestWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = (Guest)view.CurrentItem;

            if (selectedGuest != null)
            {
                var editGuestWindow = new AddEditGuest(selectedGuest);

                Hide();
                if (editGuestWindow.ShowDialog() == true)
                {
                    FillData();
                }
                Show();
            }
            else
            {
                MessageBox.Show("Please select user before trying to edit.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = view.CurrentItem as Guest;

            if (selectedGuest != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {selectedGuest.Name}?", "Submit warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    
                    guestService.DeleteGuest(selectedGuest.Id);
                    FillData();
                }
            }
            else
            {
                MessageBox.Show("Please selec t user you want to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void GuestSearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Filter = null;
            int guestId;

            if (int.TryParse(GuestSearchTB.Text, out guestId))
            {
                view.Filter = guestObject => ((Guest)guestObject).Id == guestId;
            }
        }

        private void GuestDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower() || e.PropertyName.ToLower() == "Password".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
