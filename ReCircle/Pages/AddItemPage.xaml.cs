using Newtonsoft.Json;
using ReCircle.Classes;
using ReCircle.Classes.http;
using ReCircle.Classes.http.dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class AddItemPage : Page
    {
        public AddItemPage()
        {
            this.InitializeComponent();
            Init();
        }


        private async void Init()
        {
            try
            {
                //Debug.WriteLine(authors.ToString());
            }
            catch (JsonReaderException ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                LoadingIndicator.IsActive = false;
                LoadingIndicator.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            string Title = string.Empty;
            string Description = string.Empty;
            string ImageUrl = string.Empty;

            if (TitleTextBox.Text.Equals(""))
            {
                NeedTitle.Visibility = Visibility.Visible;
                TitleTextBox.PlaceholderForeground = new SolidColorBrush(Colors.Red);
            }
            else if (DescriptionTextBox.Text.Equals(""))
            {
                NeedTitle.Visibility = Visibility.Collapsed;

                NeedDescription.Visibility = Visibility.Visible;
                DescriptionTextBox.PlaceholderForeground = new SolidColorBrush(Colors.Red);
            }
            else if (UrlTextBox.Text.Equals(""))
            {
                NeedDescription.Visibility = Visibility.Collapsed;

                NeedURL.Visibility = Visibility.Visible;
                UrlTextBox.PlaceholderForeground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                NeedURL.Visibility = Visibility.Collapsed;

                Title = TitleTextBox.Text;
                Description = DescriptionTextBox.Text;
                ImageUrl = UrlTextBox.Text;
            }
            try
            {
                Item newItem = new Item { Title = TitleTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    PhotoUrl = new Uri(UrlTextBox.Text),
                    OwnerNickname = "Denielle",
                    OwnerEmail = "denielle's email fucks sake" };


                DummyData.AddItem(newItem);

                /*ItemForCreationDto dto = new ItemForCreationDto
                {
                    Title = TitleTextBox.Text,
                    Summary = DescriptionTextBox.Text,
                };

                var response = await ItemData.PostItem(dto);
                Debug.WriteLine($"Response on creation of book: {response}");*/
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred creating the data transfer object: {ex}");
            }
        }       
    }
}
