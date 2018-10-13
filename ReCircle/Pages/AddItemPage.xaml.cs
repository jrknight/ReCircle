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
                AuthorList.Visibility = Visibility.Collapsed;
                //Debug.WriteLine(authors.ToString());
            }
            catch (JsonReaderException ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                AuthorList.Visibility = Visibility.Visible;
                LoadingIndicator.IsActive = false;
                LoadingIndicator.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {

            string Title = string.Empty;
            string Summary = string.Empty;
            ArrayList genreIds = new ArrayList();
            string Isbn = string.Empty;

            if (TitleTextBox.Text.Equals(""))
            {
                TitleTextBox.PlaceholderText = "You must provide a title value.";
                TitleTextBox.PlaceholderForeground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                Title = TitleTextBox.Text;

                //Check fields and add items
            }

            try
            {
                ItemForCreationDto dto = new ItemForCreationDto
                {
                    Title = TitleTextBox.Text,
                    Summary = SummaryTextBox.Text,
                    ISBN = IsbnTextBox.Text,
                    GenreIds = genreIds
                };

                var response = await ItemData.PostItem(dto);
                Debug.WriteLine($"Response on creation of book: {response}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred creating the data transfer object: {ex}");
            }
        }

        private void OpenNewGenre_Click(object sender, RoutedEventArgs e)
        {
            if (NewGenreMenu.Visibility == Visibility.Collapsed)
            {
                NewGenreMenu.Visibility = Visibility.Visible;
            }
            else
            {
                NewGenreMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenNewAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (NewAuthorMenu.Visibility == Visibility.Collapsed)
            {
                NewAuthorMenu.Visibility = Visibility.Visible;
            }
            else
            {
                NewAuthorMenu.Visibility = Visibility.Collapsed;
            }
        }

        
    }
}
