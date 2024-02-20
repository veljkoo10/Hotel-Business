using HotelReservations.Exceptions;
using HotelReservations.Model;
using HotelReservations.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservations.Service
{
    public class ReservationService
    {
        IReservationRepository reservationRepository;

        public ReservationService(IReservationRepository repository)
        {
            reservationRepository = repository;
        }
        public List<Reservation> GetAllReservations()
        {
            var reservationList = new List<Reservation>();
            foreach (var reservation in Hotel.GetInstance().Reservations)
            {
                if (reservation.IsActive)
                {
                    reservationList.Add(reservation);
                }
            }
            return reservationList;
        }
        public Reservation GetReservation(int id)
        {
            foreach (var reservation in GetAllReservations())
            {
                if (reservation.Id == id)
                {
                    return reservation;
                }

            }
            return null;
        }
        public void DeactivateReservation(int reservationId)
        {
            var reservationToDeactivate = GetReservation(reservationId);

            if (reservationToDeactivate != null)
            {
                reservationToDeactivate.IsActive = false;
                reservationRepository.Update(reservationToDeactivate);
            }

        }
        public void SaveReservation(Reservation reservation)
        {
            if (!DuplicateReservations(reservation))
            {
                try
                {
                    if (reservation.Id == 0)
                    {
                        reservation.Id = reservationRepository.Insert(reservation);
                        Hotel.GetInstance().Reservations.Add(reservation);
                    }
                    else
                    {
                        var index = Hotel.GetInstance().Reservations.FindIndex(r => r.Id == reservation.Id);
                        Hotel.GetInstance().Reservations[index] = reservation;
                    }

                    reservationRepository.Save(Hotel.GetInstance().Reservations);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving reservation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Reservation with the same data already exists.", "Duplicate Reservation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool DuplicateReservations(Reservation reservation)
        {
            return Hotel.GetInstance().Reservations.Any(existingReservation =>
                existingReservation.RoomNumber == reservation.RoomNumber &&
                existingReservation.StartDateTime == reservation.StartDateTime &&
                existingReservation.EndDateTime == reservation.EndDateTime &&
                existingReservation.ReservationType == reservation.ReservationType &&
                existingReservation.TotalPrice == reservation.TotalPrice &&
                existingReservation.IsActive);
        }

    }
}
