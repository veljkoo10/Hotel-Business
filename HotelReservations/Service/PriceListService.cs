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
    public class PriceListService
    {
        IPriceListRepository priceRepository;
        public PriceListService()
        {
            priceRepository = new PriceListRepository();
        }

        public List<Price> GetAllPrices()
        {
            var priceList = new List<Price>();
            foreach (var price in Hotel.GetInstance().PriceList)
            {
                if (price.IsActive)
                {
                    priceList.Add(price);
                }
            }
            return priceList;
        }
        public Price GetPrice(int id)
        {
            foreach (var price in GetAllPrices())
            {
                if (price.Id == id)
                {
                    return price;
                }

            }
            return null;
        }
        public List<Price> GetSortedPrice()
        {
            var price = Hotel.GetInstance().PriceList;
            price.Sort((r1, r2) => r1.Id.CompareTo(r2.Id));
            return price;
        }

        public void DeactivatePrice(int priceId) 
        {
            var pricetodel = GetPrice(priceId);

            if (pricetodel != null)
            {
                pricetodel.IsActive = false;
                priceRepository.Save(Hotel.GetInstance().PriceList);
            }
        }


        public void SavePrice(Price price)
        {
            if (ValidatePrice(price))
            {
                if (price.Id == 0)
                {
                    price.Id = priceRepository.Insert(price);
                    Hotel.GetInstance().PriceList.Add(price);
                }
                else
                {
                    var index = Hotel.GetInstance().PriceList.FindIndex(r => r.Id == price.Id);
                    Hotel.GetInstance().PriceList[index] = price;
                }
                priceRepository.Save(Hotel.GetInstance().PriceList);
            }
        }


        private bool ValidatePrice(Price price)
        {
            var existingPrice = Hotel.GetInstance().PriceList.FirstOrDefault(
                p => p.RoomType == price.RoomType &&
                     p.ReservationType == price.ReservationType &&
                     p.PriceValue == price.PriceValue &&
                     p.IsActive);

            if (existingPrice != null)
            {
                MessageBox.Show($"Cena za odabrani Room Type, Reservation Type i vrednost već postoji.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

    }
}
