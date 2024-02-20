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
    public partial class Users : Window
    {
        private UserService userService;
        private ICollectionView view;

        public Users()
        {
            userService = new UserService();

            InitializeComponent();
            FillData();
            Loaded += Users_Loaded;
        }

        private void Users_Loaded(object sender, RoutedEventArgs e)
        {
            UsersDG.SelectedItem = null;
        }

        private void FillData()
        {
            var users = userService.GetAllUsers();
            view = CollectionViewSource.GetDefaultView(users);
            UsersDG.ItemsSource = null;
            UsersDG.ItemsSource = view;
            UsersDG.IsSynchronizedWithCurrentItem = true;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new AddEditUser();

            Hide();
            if (addUserWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = (User)view.CurrentItem;

            if (selectedUser != null)
            {
                var editUserWindow = new AddEditUser(selectedUser);

                Hide();
                if (editUserWindow.ShowDialog() == true)
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
            var selectedUser = view.CurrentItem as User;

            if (selectedUser != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {selectedUser.Username}?", "Submit warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    userService.DeleteUser(selectedUser.Id);
                    FillData(); 
                }
            }
            else
            {
                MessageBox.Show("Please select user you want to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void UserSearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Filter = null;
            int userId;

            if (int.TryParse(UserSearchTB.Text, out userId))
            {
                view.Filter = userObject => ((User)userObject).Id == userId;
            }
        }

        private void UserDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower() || e.PropertyName.ToLower() == "Password".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
