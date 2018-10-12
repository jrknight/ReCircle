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
        List<string> userTypes = new List<string>();

        public CreateAccount()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += CreateAccount_BackRequested;
            userTypes.Add("Student");
            userTypes.Add("Librarian");
        }

        private void CreateAccount_BackRequested(object sender, BackRequestedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void UserType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox b = (ComboBox)sender;

            if (b.SelectedItem.ToString() == userTypes.ElementAt(0))
            {
                IsAdminText.Visibility = Visibility.Collapsed;
            }
            else if (b.SelectedItem.ToString() == userTypes.ElementAt(1))
            {
                IsAdminText.Visibility = Visibility.Visible;
            }
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!Password.Password.Equals(""))
            {
                if (!Name.Text.Equals(""))
                {
                    if (UserId.Text.Length >= 5)
                    {
                        if (/*checks email*/ true)
                        {
                            if (Password.Password == ConfirmPassword.Password)
                            {
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
                            else
                            {
                                PasswordNotSame.Visibility = Visibility.Visible;
                                BitmapImage bitmap = new BitmapImage();
                                bitmap.UriSource = new Uri("ms-appx://LibraryApp/Assets/RedX.png");
                                PasswordMeetsRequirements.Source = bitmap;
                            }
                        }
                        else
                        {
                            EmailNotEmail.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        UsernameNotLongEnough.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    NameNotFilled.Visibility = Visibility.Visible;
                }
            }
            else
            {
                PasswordNotLongEnough.Visibility = Visibility.Visible;
            }
        }
    }
}
