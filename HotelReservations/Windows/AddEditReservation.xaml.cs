using HotelReservations.Model;
using HotelReservations.Repository;
using HotelReservations.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
    public partial class AddEditReservation : Window
    {
        private PriceListService priceService;
        public List<string> Guests;
        private ReservationService reservationService;
        private RoomService roomService;
        private Reservation contextReservation;
        private Reservation selectedReservation;

        public AddEditReservation(Reservation reservation = null)
        {

            if (reservation == null)
            {
                contextReservation = new Reservation();
            }
            else
            {
                contextReservation = reservation.Clone();
                selectedReservation = reservation; 
            }

            var reservationRepository = new ReservationRepository();
            reservationService = new ReservationService(reservationRepository);
            InitializeComponent();
            DataContext = selectedReservation; 
            priceService = new PriceListService();
            AdjustWindow(reservation);

            Debug.WriteLine($"contextReservation: {contextReservation}");

            // roomtype
            var roomTypes = Hotel.GetInstance().RoomTypes;
            ReservationTypeCB.ItemsSource = Enum.GetValues(typeof(ReservationType)).Cast<ReservationType>();

            // roomnumber
            var activeRoomNumbers = Hotel.GetInstance().Rooms
            .Where(room => room.IsActive)
            .Select(room => room.RoomNumber);
            RoomNumberComboBox.ItemsSource = activeRoomNumbers;

            // gost
            Guests = new List<string>();
            foreach (var guest in Hotel.GetInstance().Guests.Where(g => g.IsActive))
            {
                Guests.Add($"{guest.Name} {guest.Surname}");
            }
            GuestListBox.ItemsSource = Guests;


            // datum 
            StartDateTimePicker.SelectedDateChanged += StartDateTime_SelectedDateChanged;
            EndDateTimePicker.SelectedDateChanged += EndDateTime_SelectedDateChanged;
        }


        public void AdjustWindow(Reservation? reservation = null)
        {
            if (reservation != null)
            {
                Title = "Edit Reservation";

            }
            else
            {
                Title = "Add Reservation";
            }

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate())
                {
                    if (IsRoomAvailable(contextReservation.RoomNumber, contextReservation.StartDateTime, contextReservation.EndDateTime))
                    {
                        reservationService.SaveReservation(contextReservation);
                        DialogResult = true;
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving reservation: {ex.Message}");
                MessageBox.Show("An error occurred while saving the reservation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        //gost
        private void GuestListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Guest> selectedGuests = GuestListBox.SelectedItems.Cast<string>()
                                                  .Select(name => new Guest { Name = name })
                                                  .ToList();

            contextReservation.Guests = selectedGuests;
        }

        //datum
        private void StartDateTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            contextReservation.StartDateTime = StartDateTimePicker.SelectedDate ?? DateTime.MinValue;
            UpdateRoomType();
            UpdateTotalValue();
        }

        private void EndDateTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            contextReservation.EndDateTime = EndDateTimePicker.SelectedDate ?? DateTime.MinValue;
            UpdateRoomType();
            UpdateTotalValue();
        }
        //roomtype
        private void UpdateRoomType()
        {
            if (StartDateTimePicker.SelectedDate.HasValue && EndDateTimePicker.SelectedDate.HasValue)
            {
                DateTime startDate = StartDateTimePicker.SelectedDate.Value;
                DateTime endDate = EndDateTimePicker.SelectedDate.Value;

                if (startDate.Date == endDate.Date)
                {
                    contextReservation.ReservationType = ReservationType.Day;
                }
                else
                {
                    contextReservation.ReservationType = ReservationType.Night;
                }

                ReservationTypeCB.SelectedItem = contextReservation.ReservationType;
            }
        }

        //cena
        private void ReservationTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTotalValue();
        }

        private void RoomNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomNumberComboBox.SelectedItem != null)
            {
                contextReservation.RoomNumber = RoomNumberComboBox.SelectedItem.ToString();
                UpdateTotalValue();  
            }
        }


        private void UpdateTotalValue()
        {
            if (RoomNumberComboBox.SelectedItem == null || ReservationTypeCB.SelectedItem == null)
            {
                return;
            }

            string selectedRoomNumber = RoomNumberComboBox.SelectedItem as string;
            ReservationType reservationType = (ReservationType)ReservationTypeCB.SelectedItem;

            Room selectedRoom = Hotel.GetInstance().Rooms.FirstOrDefault(room => room.RoomNumber == selectedRoomNumber);

            if (selectedRoom != null)
            {
                RoomType roomType = selectedRoom.RoomType;
                decimal priceValue = GetPriceForRoomTypeAndReservationType(roomType, reservationType);
                double numberOfDays = (EndDateTimePicker.SelectedDate - StartDateTimePicker.SelectedDate)?.TotalDays ?? 0;


                if (numberOfDays == 0)
                {
                    numberOfDays = 1;
                }

                contextReservation.TotalPrice = (decimal)(priceValue * Convert.ToDecimal(numberOfDays));
                TotalValueTextBox.Text = contextReservation.TotalPrice.ToString();
            }
        }


        private decimal GetPriceForRoomTypeAndReservationType(RoomType roomType, ReservationType reservationType)
        {
            var allPrices = priceService.GetAllPrices();
            Price matchedPrice = allPrices.FirstOrDefault(p => string.Equals(p.RoomType.ToString(), roomType.ToString(), StringComparison.OrdinalIgnoreCase)
                && p.ReservationType == reservationType && p.IsActive);

            if (matchedPrice == null)
            {
                return 0;
            }
            else
            {
                return matchedPrice.PriceValue;
            }
        }



        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(contextReservation.RoomNumber) ||
                contextReservation.Guests == null || contextReservation.Guests.Count == 0 ||
                contextReservation.ReservationType == null ||
                !StartDateTimePicker.SelectedDate.HasValue ||
                !EndDateTimePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Popuni sva polja.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (contextReservation.TotalPrice == 0)
            {
                MessageBox.Show("Ne postoji cena za datu sobu.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (DateTime.Now > StartDateTimePicker.SelectedDate.Value)
            {
                MessageBox.Show("Selektovani datum početka je prošao.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (DateTime.Now > EndDateTimePicker.SelectedDate.Value)
            {
                MessageBox.Show("Selektovani datum završetka je prošao.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            IsRoomAvailable(contextReservation.RoomNumber, StartDateTimePicker.SelectedDate.Value, EndDateTimePicker.SelectedDate.Value);


            return true;
        }
        private bool IsRoomAvailable(string roomNumber, DateTime startDate, DateTime endDate)
        {
            var overlappingReservation = reservationService.GetAllReservations()
                .FirstOrDefault(r => r.RoomNumber == roomNumber && r.IsActive &&
                                     ((startDate < r.EndDateTime && startDate >= r.StartDateTime) ||
                                      (endDate > r.StartDateTime && endDate <= r.EndDateTime) ||
                                      (startDate <= r.StartDateTime && endDate >= r.EndDateTime)));

            if (overlappingReservation != null)
            {
                MessageBox.Show($"Soba je već rezervisana u periodu od {overlappingReservation.StartDateTime.ToShortDateString()} do {overlappingReservation.EndDateTime.ToShortDateString()}.", "Soba Zauzeta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }


    }
}
