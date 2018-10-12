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
        List<Author> Authors = new List<Author>();
        Author BookAuthor;
        List<Genre> ListOfGenres = new List<Genre>();
        List<Genre> BookGenres = new List<Genre>();


        private async void Init()
        {
            try
            {
                AuthorList.Visibility = Visibility.Collapsed;
                Authors = await AuthorData.GetAuthors();
                ListOfGenres = await GenreData.GetGenres();
                //Debug.WriteLine(authors.ToString());
            }
            catch (JsonReaderException ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                AuthorList.Visibility = Visibility.Visible;
                AuthorList.ItemsSource = Authors;
                GenreList.ItemsSource = ListOfGenres;
                LoadingIndicator.IsActive = false;
                LoadingIndicator.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {

            string Title = string.Empty;
            string Summary = string.Empty;
            ArrayList genreIds = new ArrayList();
            int authorId = -1;
            string Isbn = string.Empty;

            if (TitleTextBox.Text.Equals(""))
            {
                TitleTextBox.PlaceholderText = "You must provide a title value.";
                TitleTextBox.PlaceholderForeground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                Title = TitleTextBox.Text;

                if (BookAuthor == null)
                {
                    AuthorList.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    authorId = BookAuthor.Id;

                    if (SummaryTextBox.Text.Equals(""))
                    {
                        SummaryTextBox.PlaceholderText = "You must provide a summary value.";
                        SummaryTextBox.PlaceholderForeground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        Summary = SummaryTextBox.Text;

                        if (IsbnTextBox.Text.Equals(""))
                        {
                            IsbnTextBox.PlaceholderText = "You must provide a ISBN number.";
                            IsbnTextBox.PlaceholderForeground = new SolidColorBrush(Colors.Red);
                        }
                        else
                        {
                            /// needs genre functionality
                            if (!(BookGenres.Count() > 0))
                            {
                                GenreList.BorderBrush = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                foreach (Genre g in ListOfGenres)
                                {
                                    genreIds.Add(g.Id);
                                }
                            }
                        }
                    }
                }
            }

            try
            {
                BookForCreationDto dto = new BookForCreationDto
                {
                    Title = TitleTextBox.Text,
                    AuthorId = BookAuthor.Id,
                    Summary = SummaryTextBox.Text,
                    ISBN = IsbnTextBox.Text,
                    GenreIds = genreIds
                };

                var response = await ItemData.PostBook(dto);
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

        private async void NewAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            if (Authors.Any(auth => auth.Name == NewAuthorTextBox.Text))
            {
                return; // TODO: needs a prompt to say that the author already exists
            }
            Author a = new Author
            {
                Name = NewAuthorTextBox.Text
            };
            var response = await AuthorData.PostAuthor(a);
            Debug.WriteLine(response);
            Init();
        }

        private async void NewGenreButton_Click(object sender, RoutedEventArgs e)
        {
            // query adding new genre
            if (ListOfGenres.Any(gn => gn.Name == NewGenreTextBox.Text))
            {
                return;
            }
            Genre g = new Genre
            {
                Name = NewGenreTextBox.Text
            };
            var response = await GenreData.PostGenre(g);
            Init();
        }

        /*private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton r = (RadioButton) sender;
            Genre g = new Genre {
                Name = r.Content.ToString()
            };
            BookGenres.Add(g);
            Debug.WriteLine(BookGenres.ToString());
        }*/

        private void GenreList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListOfGenres.Add((Genre)e.ClickedItem);
        }

        private void AuthorList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem;
            BookAuthor = (Author)item;
        }
    }
}
