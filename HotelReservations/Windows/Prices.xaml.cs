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
    /// <summary>
    /// Interaction logic for Prices.xaml
    /// </summary>
    public partial class Prices : Window
    {
        private ICollectionView? view;
        public Prices()
        {
            InitializeComponent();
            FillData();
        }

        public void FillData()
        {
            var priceService = new PriceListService();
            var priceList = priceService.GetAllPrices();

            view = CollectionViewSource.GetDefaultView(priceList);
            view.Filter = DoFilter;
            view.Refresh();
            PriceDataGrid.ItemsSource = null;
            PriceDataGrid.ItemsSource = view;
            PriceDataGrid.IsSynchronizedWithCurrentItem = true;
            PriceDataGrid.SelectedItem = null;
        }

        private void PriceIdSearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void PriceDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private bool DoFilter(object priceObject)
        {
            var price = priceObject as Price;

            var priceIdSearchParam = PriceIdSearchTB.Text;

            if (price!.Id.ToString().Contains(priceIdSearchParam))
            {
                return true;
            }

            return false;
        }

        private void AddPriceBtn_Click(object sender, RoutedEventArgs e)
        {
            var addPriceWindow = new AddEditPrice();

            Hide();
            if (addPriceWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditPriceBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedPrice = (Price)view.CurrentItem;

            if (selectedPrice != null)
            {
                var editPriceWindow = new AddEditPrice(selectedPrice);

                Hide();

                if (editPriceWindow.ShowDialog() == true)
                {
                    FillData();
                }

                Show();
            }
            else
            {
                MessageBox.Show("Please select a price to edit.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeletePriceBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedPrice = view.CurrentItem as Price;

            if (selectedPrice != null)
            {
                var confirmationResult = MessageBox.Show($"Are you sure that you want to delete price {selectedPrice.Id}?",
                    "Confirmation", MessageBoxButton.YesNo);

                if (confirmationResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        var priceService = new PriceListService();
                        priceService.DeactivatePrice(selectedPrice.Id); 
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
                MessageBox.Show("Please select a price to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
