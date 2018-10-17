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
    public sealed partial class ItemStatusPage : Page
    {
        private List<Item> ItemRequests = new List<Item>();
        private List<Item> ItemRecords = new List<Item>();
        private List<ItemRequest> AllItemRequests = new List<ItemRequest>();
        private List<ItemRecord> AllItemRecords = new List<ItemRecord>();
        private dynamic Selected;


        public ItemStatusPage()
        {
            this.InitializeComponent();
            SearchBox.KeyDown += new KeyEventHandler(SearchBox_KeyDown);
            LoadingIndicator.IsActive = true;
            Init();
        }

        public async void Init()
        {
            
            try
            {
                //var content = await ItemData.GetUsersItemRequests();

                /*foreach (ItemRequest i in content)
                {
                    ItemRequests.Add(i.Item);
                }
                var content2 = await ItemData.GetCheckedOutItems();
                ItemRecords = new List<Item>();
                foreach (ItemRecord i in content2)
                {
                    ItemRecords.Add(i.Item);
                }*/

                //BookRequestsList.ItemsSource = DummyData.Instance.RequestedItems;
                //CheckedOutBooksList.ItemsSource = DummyData.Instance.SubmittedItems;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred on home page initialization: {ex}");
            }
            finally
            {
                LoadingIndicator.IsActive = false;
                LoadingIndicator.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            
            
            try
            {
                //TODO: Add method to show all checked out books and the students with them ---- must have an option to printout as well
                var content = await ItemData.GetAllItemRequests();
                List<Item> items = new List<Item>();
                AllItemRequests = content;
                foreach (ItemRequest i in AllItemRequests)
                {
                    items.Add(i.Item);
                }
                BookRequestsList.ItemsSource = items;

                var contentCheckedOut = await ItemData.GetAllCheckedOutItems();
                AllItemRecords = contentCheckedOut;

                List<Item> itemsCheckedOut = new List<Item>();
                foreach (ItemRecord i in AllItemRecords)
                {
                    itemsCheckedOut.Add(i.Item);
                }
                CheckedOutBooksList.ItemsSource = itemsCheckedOut;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred on home page initialization: {ex}");
            }
            finally
            {
                LoadingIndicator.IsActive = false;
                LoadingIndicator.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            
        }

        private void AvailableBooksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var Item = e.ClickedItem;

                ItemRequest item = (ItemRequest)Item;

                Selected = AllItemRequests.Where(i => i.ItemId == item.Id).FirstOrDefault();

                PopulateItemDetails(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void PopulateItemDetails(ItemRequest item) //Populate the details page on the home page
        {
            txtTitle.Text = item.Item.Title;
            txtDescription.Text = "Description: " + item.Item.Description;
            txtOwner.Text = "Owner: " + item.Item.OwnerUsername;
            txtEmail.Text = "Email: " + item.Item.OwnerEmail;
            image.Source = new BitmapImage(item.Item.PhotoUrl);
        }

        private void PopulateItemDetails(Item item) //Populate the details page on the home page
        {
            txtTitle.Text = item.Title;
            txtDescription.Text = "Description: " + item.Description;
            txtOwner.Text = "Owner: " + item.OwnerUsername;
            txtEmail.Text = "Email: " + item.OwnerEmail;
            image.Source = new BitmapImage(item.PhotoUrl);
        }


        private void SearchBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            LightUpdateItemsList(SearchBox.Text);
        }

        private void LightUpdateItemsList(string search)
        {

            List<Item> itemList = ItemRequests;
            search = search.ToLower();

            itemList = itemList.Where(i => i.Title.ToLower().Contains(search)).ToList();

            BookRequestsList.ItemsSource = itemList;

        }

        private async void CheckInBook_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ItemData.TurnInItem(new BookRecordDto()
            {
                UserId = Selected.UserId
            });
        }

        private void CheckedOutBooksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var Item = e.ClickedItem;

                Item item = (Item)Item;

                Selected = AllItemRecords.Where(i => i.ItemId == item.Id).FirstOrDefault();

                PopulateItemDetails(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
