using HotelReservations.Model;
using HotelReservations.Repository;
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
    /// <summary>
    /// Interaction logic for Reservations.xaml
    /// </summary>
    public partial class Reservations : Window
    {
        private ICollectionView? view;

        public Reservations()
        {
            InitializeComponent();
            FillData();
        }

        public void FillData()
        {
            var reservationRepository = new ReservationRepository();
            var reservationService = new ReservationService(reservationRepository);
            var reservationList = reservationService.GetAllReservations();
            view = CollectionViewSource.GetDefaultView(reservationList);
            view.Filter = DoFilter;
            view.Refresh();
            ReservationDataGrid.ItemsSource = null;
            ReservationDataGrid.ItemsSource = view;
            ReservationDataGrid.IsSynchronizedWithCurrentItem = true;
            ReservationDataGrid.SelectedItem = null;
        }

        private bool DoFilter(object reservationObject)
        {
            var reservation = reservationObject as Reservation;

            var reservationIdSearchParam = ReservationsIdSearchTB.Text;

            if (reservation!.Id.ToString().Contains(reservationIdSearchParam))
            {
                return true;
            }

            return false;
        }
        private void ReservationsIdSearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }
        private void AddResevationBtn_Click(object sender, RoutedEventArgs e)
        {
            var addReservationWindow = new AddEditReservation();

            Hide();
            if (addReservationWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditResevationBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservation = (Reservation)view.CurrentItem;

            if (selectedReservation != null)
            {
                var editReservationWindow = new AddEditReservation(selectedReservation);

                Hide();

                if (editReservationWindow.ShowDialog() == true)
                {
                    FillData();
                }

                Show();
            }
            else
            {
                MessageBox.Show("Please select a reservation to edit.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void DeleteResevationBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservation = view.CurrentItem as Reservation;

            if (selectedReservation != null)
            {
                var confirmationResult = MessageBox.Show($"Are you sure that you want to delete reservation {selectedReservation.Id}?",
                    "Confirmation", MessageBoxButton.YesNo);

                if (confirmationResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        var reservationRepository = new ReservationRepository();  
                        var reservationService = new ReservationService(reservationRepository);  

                        reservationService.DeactivateReservation(selectedReservation.Id);
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
                MessageBox.Show("Please select a reservation to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ReservationDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "guests" || e.PropertyName.ToLower() == "isactive")
            {
                e.Cancel = true;
            }
        }

    }
}
