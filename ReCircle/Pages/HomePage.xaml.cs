using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ReCircle.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class HomePage : Page
    {
        private bool isStudent;

        public HomePage()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(ItemStatusPage));

            /*if (Authentication.Instance.Role.Any(r => r.ToLower().Equals("student")))
            {
                isStudent = true;
                AddBookButton.Visibility = Visibility.Collapsed;
                CheckOutBook.Visibility = Visibility.Collapsed;
                PrintReport.Visibility = Visibility.Collapsed;
            }
            else if (Authentication.Instance.Role.Any(r => r.ToLower().Equals("librarian") || r.ToLower().Equals("teacher")))
            {
                isStudent = false;
                AddBookButton.Visibility = Visibility.Visible;
                CheckOutBook.Visibility = Visibility.Visible;
                PrintReport.Visibility = Visibility.Collapsed;
            }*/
        }
        

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            _splitView.IsPaneOpen = !_splitView.IsPaneOpen;
        }

        private void _btnHome_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ItemStatusPage));
            _txtPageLabel.Text = "Home";
        }

        private void _btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ItemListPage));
            _txtPageLabel.Text = "Browse";
        }

        private void _btnSettings_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(SettingsPage));
            _txtPageLabel.Text = "Settings";
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(AddItemPage));
        }

        private void CheckOutBook_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ReserveItemPage));
        }

        private async void Help_Click(object sender, RoutedEventArgs e)
        {
            await helpDialog.ShowAsync();
        }

        private void PrintReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

            credit.Text = Authentication.Instance.CurrentUser.Credits.ToString();
        }

        /**
        * Should check permissions and display what is necessary based on this.
        * Needs to display what the student has checked out now
        * Question what a teacher would want to see - possibly what they have checked out as well.
        */
    }
}
