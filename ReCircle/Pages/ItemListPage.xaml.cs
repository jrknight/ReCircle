﻿using ReCircle.Classes;
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
        private List<Item> DummyData = new List<Item>();

        public ItemListPage()
        {
            this.InitializeComponent();

            Init();
        }

        public async void Init()
        {
            DummyData.Add(new Item { Title = "Item", OwnerNickname = "Josh", Description = "Like new", CurrentHolderNickname = "Elijah", PhotoUrl = new Uri("https://cloud.netlifyusercontent.com/assets/344dbf88-fdf9-42bb-adb4-46f01eedd629/242ce817-97a3-48fe-9acd-b1bf97930b01/09-posterization-opt.jpg") });
            DummyData.Add(new Item { Title = "Shoes", OwnerNickname= "Colton", Description = "Used, no use for them now", CurrentHolderNickname = "Colton", PhotoUrl = new Uri("https://cloud.netlifyusercontent.com/assets/344dbf88-fdf9-42bb-adb4-46f01eedd629/242ce817-97a3-48fe-9acd-b1bf97930b01/09-posterization-opt.jpg") });
            DummyData.Add(new Item { Title = "Sunglasses", OwnerNickname = "Denielle", Description = "No scratches, polarized", CurrentHolderNickname = "Elijah", PhotoUrl = new Uri("https://cloud.netlifyusercontent.com/assets/344dbf88-fdf9-42bb-adb4-46f01eedd629/242ce817-97a3-48fe-9acd-b1bf97930b01/09-posterization-opt.jpg") });
            DummyData.Add(new Item { Title = "Hat", OwnerNickname = "Josh", Description = "Looks fine, works like a hat", CurrentHolderNickname = "Josh", PhotoUrl = new Uri("https://cloud.netlifyusercontent.com/assets/344dbf88-fdf9-42bb-adb4-46f01eedd629/242ce817-97a3-48fe-9acd-b1bf97930b01/09-posterization-opt.jpg") });
            DummyData.Add(new Item { Title = "Coffee Mug", OwnerNickname = "Denielle", Description = "Its a coffee mug", CurrentHolderNickname = "Dave! Yognaught", PhotoUrl = new Uri("https://cloud.netlifyusercontent.com/assets/344dbf88-fdf9-42bb-adb4-46f01eedd629/242ce817-97a3-48fe-9acd-b1bf97930b01/09-posterization-opt.jpg") });
            DummyData.Add(new Item { Title = "HydroFlask", OwnerNickname = "Josh", Description = "Like new", CurrentHolderNickname = "Elijah", PhotoUrl = new Uri("https://cloud.netlifyusercontent.com/assets/344dbf88-fdf9-42bb-adb4-46f01eedd629/242ce817-97a3-48fe-9acd-b1bf97930b01/09-posterization-opt.jpg") });

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
                    Items = DummyData;
                    AvailableBooksList.ItemsSource = Items;
                    AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
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
            txtDescription.Text = item.Description;

            CurrentlyDisplayed = item;
            AuthorBooksList.Visibility = Visibility.Collapsed;
            BooksWritten.Visibility = Visibility.Collapsed;
            RequestBookButton.Visibility = Visibility.Visible;
        }

        private void PopulateOwnerDetails(User user)
        {
            txtTitle.Text = user.Name;

            //AuthorBooksList.ItemsSource = user.BooksWritten.ToList();
            AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Visible;
            BooksWritten.Visibility = Visibility.Visible;
            RequestBookButton.Visibility = Visibility.Collapsed;
        }

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
            var bookRequest = new BookRequestDto()
            {
                BookId = CurrentlyDisplayed.Id
            };

            var response = await ItemData.PostNewItemRequest(bookRequest);

        }
    }
}
