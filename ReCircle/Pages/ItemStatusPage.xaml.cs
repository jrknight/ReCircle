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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ReCircle.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ItemDetailsPage : Page
    {
        private List<Item> ItemRequests = new List<Item>();
        private List<Item> ItemRecords = new List<Item>();
        private List<ItemRequest> AllItemRequests = new List<ItemRequest>();
        private List<ItemRecord> AllItemRecords = new List<ItemRecord>();
        private dynamic Selected;
        private bool isStudent;


        public ItemDetailsPage()
        {
            this.InitializeComponent();
            SearchBox.KeyDown += new KeyEventHandler(SearchBox_KeyDown);
            LoadingIndicator.IsActive = true;
            if (Authentication.Instance.Role.Any(r => r.ToLower().Equals("student")))
            {
                isStudent = true;
                Init(true);
            }
            else if (Authentication.Instance.Role.Equals("librarian"))
            {
                isStudent = false;
                Init(false);
            }
        }

        public async void Init(bool isStudent)
        {
            if (isStudent)
            {
                try
                {
                    var content = await ItemData.GetUsersItemRequests();
                    ItemRequests = new List<Item>();
                    foreach (ItemRequest i in content)
                    {
                        ItemRequests.Add(i.Item);
                    }
                    var content2 = await ItemData.GetCheckedOutItems();
                    ItemRecords = new List<Item>();
                    foreach (ItemRecord i in content2)
                    {
                        ItemRecords.Add(i.Item);
                    }
                    ItemRequestsList.ItemsSource = ItemRequests;
                    CheckedOutItemsList.ItemsSource = ItemRecords;
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
            else
            {
                try
                {
                    //TODO: Add method to show all checked out books and the students with them ---- must have an option to printout as well
                    var content = await ItemData.GetAllItemRequests();
                    List<Item> items = new List<Item>();
                    AllItemRequests = content;
                    StudentCheckedOut.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    foreach (ItemRequest i in AllItemRequests)
                    {
                        items.Add(i.Item);
                    }
                    ItemRequestsList.ItemsSource = items;

                    var contentCheckedOut = await ItemData.GetAllCheckedOutItems();
                    AllItemRecords = contentCheckedOut;

                    List<Item> itemsCheckedOut = new List<Item>();
                    foreach (ItemRecord i in AllItemRecords)
                    {
                        itemsCheckedOut.Add(i.Item);
                    }
                    CheckedOutItemsList.ItemsSource = itemsCheckedOut;
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
        }

        private void AvailableBooksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            CheckInBook.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            try
            {
                var Item = e.ClickedItem;

                Item item = (Item)Item;

                Selected = AllItemRequests.Where(i => i.ItemId == item.Id).FirstOrDefault();

                PopulateItemDetails(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void PopulateItemDetails(Item item) //Populate the details page on the home page
        {
            item = await ItemData.GetItem(item.Id);

            txtTitle.Text = item.Title;
            txtDescription.Text = item.Description;

            AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (!isStudent && Selected.GetType() == typeof(ItemRequest))
            {
                //var userName = $"Requested by {AllItemRequests?.Where(b => b.BookId == book.Id).FirstOrDefault().User.Name}";
                //StudentCheckedOut.Text = userName.ToString();
            }
        }

        /*private void PopulateAuthorDetails(Author author)
        {
            txtTitle.Text = author.Name;

            AuthorBooksList.ItemsSource = author.BooksWritten.ToList();
            AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }*/

        /*private async void TxtAuthor_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            HyperlinkButton b = (HyperlinkButton)sender;
            var response = await AuthorData.GetBooksWithAuthor(Selected.Book.AuthorId);

            Author a = new Author()
            {
                Name = b.Content.ToString(),
                BooksWritten = response
            };
            PopulateAuthorDetails(a);
        }*/

        private void SearchBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            LightUpdateItemsList(SearchBox.Text);
        }

        private void LightUpdateItemsList(string search)
        {
            AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            List<Item> itemList = ItemRequests;
            search = search.ToLower();

            itemList = itemList.Where(i => i.Title.ToLower().Contains(search)).ToList();

            ItemRequestsList.ItemsSource = itemList;

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
            AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            CheckInBook.Visibility = Windows.UI.Xaml.Visibility.Visible;

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
