using ReCircle.Classes;
using ReCircle.Classes.http;
using ReCircle.Classes.http.dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ReCircle.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateAccount : Page
    {

        public CreateAccount()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += CreateAccount_BackRequested;
        }

        private void CreateAccount_BackRequested(object sender, BackRequestedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void UserType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {

            if (Name.Text.Equals(""))
            {
                Name.PlaceholderForeground = new SolidColorBrush(Colors.Red);
                NameNotFilled.Visibility = Visibility.Visible;
            }
            else if (UserId.Text.Length < 5)
            {
                NameNotFilled.Visibility = Visibility.Collapsed;

                UserId.PlaceholderForeground = new SolidColorBrush(Colors.Red);
                UsernameNotLongEnough.Visibility = Visibility.Visible;

            }
            //Also check for email being real
            else if (Email.Text.Equals(""))
            {
                UsernameNotLongEnough.Visibility = Visibility.Collapsed;

                Email.PlaceholderForeground = new SolidColorBrush(Colors.Red);
                EmailNotEmail.Visibility = Visibility.Visible;
            }
            else if (Password.Password.Length < 8)
            {
                EmailNotEmail.Visibility = Visibility.Collapsed;

                Password.Password = "";
                PasswordNotLongEnough.Visibility = Visibility.Visible;
            }
            else if (!(Password.Password == ConfirmPassword.Password))
            {
                PasswordNotLongEnough.Visibility = Visibility.Collapsed;

                ConfirmPassword.Password = "";
                PasswordNotSame.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordNotLongEnough.Visibility = Visibility.Collapsed;
                EmailNotEmail.Visibility = Visibility.Collapsed;
                UsernameNotLongEnough.Visibility = Visibility.Collapsed;
                NameNotFilled.Visibility = Visibility.Collapsed;
                PasswordNotSame.Visibility = Visibility.Collapsed;

                var x = new UserModelDto()
                {
                    Email = Email.Text,
                    Name = Name.Text,
                    Password = Password.Password,
                    UserName = UserId.Text
                };
                try
                {
                    var response = await Authentication.NewUser(x);
                    if (response.StatusCode == Windows.Web.Http.HttpStatusCode.Created)
                    {
                        FailedMessage.Visibility = Visibility.Collapsed;
                        Frame.Navigate(typeof(MainPage), x);
                    }
                    else
                    {
                        FailedMessage.Visibility = Visibility.Visible;
                        FailedMessage.Text = $"There was a problem creating the user: {response.ReasonPhrase}";
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"An exception occurred on creating a new user: {ex}");
                }
                return;
            }
        }
    }
}
