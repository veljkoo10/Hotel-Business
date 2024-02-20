using HotelReservations.Exceptions;
using HotelReservations.Model;
using HotelReservations.Repository;
using System;
using System.Collections.Generic;
using System.IO;

namespace HotelReservations
{
    public class DataUtil
    {
        public static void LoadData()
        {
            Hotel hotel = Hotel.GetInstance();
            hotel.Id = 1;
            hotel.Name = "Hotel Park";
            hotel.Address = "Kod Futoskog parka...";
            // Učitavanje tipa sobe
            try
            {
                IRoomTypeRepository roomtypeRepository = new RoomTypeRepository();
                var loadedroomtype = roomtypeRepository.GetAll();

                if (loadedroomtype != null)
                {
                    Hotel.GetInstance().RoomTypes = loadedroomtype;
                }
            }
            catch (CouldntLoadResourceException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
            catch (Exception ex)
            {
                Console.Write("An unexpected error occured", ex.Message);
            }
            // Učitavanje soba
            try
            {
                IRoomRepository roomRepository = new RoomRepository();
                var loadedRooms = roomRepository.GetAll();

                if (loadedRooms != null)
                {
                    Hotel.GetInstance().Rooms = loadedRooms;
                }
            }
            catch (CouldntLoadResourceException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
            catch (Exception ex)
            {
                Console.Write("An unexpected error occured", ex.Message);
            }

            // Učitavanje korisnika
            try
            {
                IUsersRepository userRepository = new UsersRepository();
                var loadedUsers = userRepository.LoadAll();

                if (loadedUsers != null)
                {
                    Hotel.GetInstance().Users = loadedUsers;
                }
            }
            catch (CouldntLoadResourceException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
            catch (Exception ex)
            {
                Console.Write("An unexpected error occured", ex.Message);
            }
            // Učitavanje gosta
            try
            {
                IGuestRepository guestRepository = new GuestRepository();
                var loadedGuest = guestRepository.Load();

                if (loadedGuest != null)
                {
                    Hotel.GetInstance().Guests = loadedGuest;
                }
            }
            catch (CouldntLoadResourceException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
            catch (Exception ex)
            {
                Console.Write("An unexpected error occured", ex.Message);
            }
            // Učitavanje price
            try
            {
                IPriceListRepository PriceRepository = new PriceListRepository();
                var loadedprices = PriceRepository.GetAll();

                if (loadedprices != null)
                {
                    Hotel.GetInstance().PriceList = loadedprices;
                }
            }
            catch (CouldntLoadResourceException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
            catch (Exception ex)
            {
                Console.Write("An unexpected error occured", ex.Message);
            }
            // Učitavanje rezervacija
            try
            {
                IReservationRepository ReservationRepository = new ReservationRepository();
                var loadedreservations = ReservationRepository.Load();

                if (loadedreservations != null)
                {
                    Hotel.GetInstance().Reservations = loadedreservations;
                }
            }
            catch (CouldntLoadResourceException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
            catch (Exception ex)
            {
                Console.Write("An unexpected error occured", ex.Message);
            }
        }

        public static void PersistData()
        {
            try
            {
                // Čuvanje soba
                IRoomRepository roomRepository = new RoomRepository();
                roomRepository.Save(Hotel.GetInstance().Rooms);

                // Čuvanje korisnika
                IUsersRepository userRepository = new UsersRepository();
                userRepository.Save(Hotel.GetInstance().Users);
                // Čuvanje tipa sobe
                IRoomTypeRepository RoomTypeRepository = new RoomTypeRepository();
                RoomTypeRepository.Save(Hotel.GetInstance().RoomTypes);
                // Čuvanje gosta
                IGuestRepository guestRepository = new GuestRepository();
                guestRepository.Save(Hotel.GetInstance().Guests);
                // Čuvanje cene
                IPriceListRepository priceRepository = new PriceListRepository();
                priceRepository.Save(Hotel.GetInstance().PriceList);
                // Čuvanje rezervacije
                IReservationRepository reservationRepository = new ReservationRepository();
                reservationRepository.Save(Hotel.GetInstance().Reservations);
            }
            catch (CouldntPersistDataException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
        }
    }
}
