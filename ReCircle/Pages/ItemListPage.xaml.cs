using ReCircle.Classes;
using ReCircle.Classes.http;
using ReCircle.Classes.http.dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ItemListPage : Page
    {
        private List<Item> Items = new List<Item>();
        private Item CurrentlyDisplayed;

        public ItemListPage()
        {
            this.InitializeComponent();
            Init();
        }

        public async void Init()
        {

            try
            {
                LoadingIndicator.IsActive = true;
                await PopulateListOfItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"There was a problem on ititialization: {ex}");
            }
            finally
            {
                LoadingIndicator.IsActive = false;
                LoadingIndicator.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private async Task PopulateListOfItems()
        {
            /// will fetch and return a list of books based on something similar to http://bit.ly/2iTrk2h
            /// should also bind list to the list in the page
            /// query for books that are currently checked out by the user 

            try
            {
                await UpdateItems(null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"There was a problem populating the list of books: {ex}");
                //TODO: Notify user of failure
            }

        }

        private async Task UpdateItems(string search)
        {
            if (search == null)
            {
                try
                {
                    //Items = await ItemData.GetItems();
                    Items = DummyData.Instance.BrowsePage;
                    AvailableBooksList.ItemsSource = Items;
                    //AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    if (Items.Count > 0)
                    {
                        PopulateItemDetails(Items.ElementAt(0));
                    }
                    else
                    {
                        List<Item> book = new List<Item>();
                        book.Add(new Item()
                        {
                            Title = "No books here, you should add some!"
                        });
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception Occurred \n\n\n\n{ex}");
                }

            }
            else
            {
                Items = await ItemData.GetItems();
                Items = Items.Where(b => b.Title.Contains(search)).ToList();
            }

            /*catch(FlurlHttpException ex)
            {
                Debug.WriteLine("Fatal Exeption: " + ex);
            }*/

            AvailableBooksList.ItemsSource = Items;
        }

        private void AvailableBooksList_ItemClick(object sender, ItemClickEventArgs e)
        {

            try
            {
                var item = e.ClickedItem;

                Item book = (Item)item;

                CurrentlyDisplayed = book;

                PopulateItemDetails(book);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"There was a problem on displaying the item details: {ex}");
            }

        }

        private void PopulateItemDetails(Item item) //Populate the details page on the home page
        {
            txtTitle.Text = item.Title;
            txtDescription.Text = "Description: " + item.Description;
            txtOwner.Text = "Owner: " + item.OwnerNickname;
            txtEmail.Text = "Email: " + item.OwnerEmail;
            image.Source = new BitmapImage(item.PhotoUrl);

            CurrentlyDisplayed = item;
            //AuthorBooksList.Visibility = Visibility.Collapsed;
            //BooksWritten.Visibility = Visibility.Collapsed;
            RequestBookButton.Visibility = Visibility.Visible;
        }

        private void PopulateOwnerDetails(User user)
        {
            txtTitle.Text = user.Name;

            //AuthorBooksList.ItemsSource = user.BooksWritten.ToList();
            //AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //BooksWritten.Visibility = Visibility.Visible;
            RequestBookButton.Visibility = Visibility.Collapsed;
        }

        /*private async void TxtAuthor_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            HyperlinkButton b = (HyperlinkButton)sender;
            txtDescription.Text = "";
            txtAuthor.Content = "";
            BooksWritten.Visibility = Visibility.Visible;
        }*/

        private async void SearchBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                await UpdateItems(SearchBox.Text);
            }
            else
            {
                LightUpdateBooksList(SearchBox.Text);
            }


        }

        private void LightUpdateBooksList(string search)
        {
            List<Item> bookList = Items;
            search = search.ToLower();

            bookList = bookList.Where(b => b.Title.ToLower().Contains(search)).ToList();

            AvailableBooksList.ItemsSource = bookList;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            /*var bookRequest = new BookRequestDto()
            {
                BookId = CurrentlyDisplayed.Id
            };

            var response = await ItemData.PostNewItemRequest(bookRequest);*/
            DummyData.RequestItem(CurrentlyDisplayed);

        }
    }
}
