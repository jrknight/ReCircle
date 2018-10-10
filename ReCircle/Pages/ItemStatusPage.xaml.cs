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
        private List<Book> BookRequests = new List<Book>();
        private List<Book> BookRecords = new List<Book>();
        private List<Author> Authors = new List<Author>();
        private List<BookRequest> AllBookRequests = new List<BookRequest>();
        private List<BookRecord> AllBookRecords = new List<BookRecord>();
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
                    var content = await BookData.GetUsersBookRequests();
                    BookRequests = new List<Book>();
                    foreach (BookRequest b in content)
                    {
                        BookRequests.Add(b.Book);
                    }
                    var content2 = await BookData.GetCheckedOutBooks();
                    BookRecords = new List<Book>();
                    foreach (BookRecord b in content2)
                    {
                        BookRecords.Add(b.Book);
                    }
                    BookRequestsList.ItemsSource = BookRequests;
                    CheckedOutBooksList.ItemsSource = BookRecords;
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
                    var content = await BookData.GetAllBookRequests();
                    List<Book> books = new List<Book>();
                    AllBookRequests = content;
                    StudentCheckedOut.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    foreach (BookRequest b in AllBookRequests)
                    {
                        books.Add(b.Book);
                    }
                    BookRequestsList.ItemsSource = books;

                    var contentCheckedOut = await BookData.GetAllCheckedOutBooks();
                    AllBookRecords = contentCheckedOut;

                    List<Book> booksCheckedOut = new List<Book>();
                    foreach (BookRecord b in AllBookRecords)
                    {
                        booksCheckedOut.Add(b.Book);
                    }
                    CheckedOutBooksList.ItemsSource = booksCheckedOut;
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
                var item = e.ClickedItem;

                Book book = (Book)item;

                Selected = AllBookRequests.Where(b => b.BookId == book.Id).FirstOrDefault();

                PopulateBookDetails(book);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void PopulateBookDetails(Book book) //Populate the details page on the home page
        {
            book = await BookData.GetBook(book.Id);
            book.Author = await AuthorData.GetAuthor(book.AuthorId);

            txtTitle.Text = book.Title;
            //Author author = Authors.Where(a => a.Id == book.AuthorId).FirstOrDefault();
            txtAuthor.Content = book.Author.Name;
            txtDescription.Text = book.Summary;
            string s = "";/*
            foreach (var g in book.BookGenres)
            {
                s += (g.Genre.Name + ", ");
            }*/

            txtGenre.Text = s;
            AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (!isStudent && Selected.GetType() == typeof(BookRequest))
            {
                var userName = $"Requested by {AllBookRequests?.Where(b => b.BookId == book.Id).FirstOrDefault().User.Name}";
                StudentCheckedOut.Text = userName.ToString();
            }
        }

        private void PopulateAuthorDetails(Author author)
        {
            txtTitle.Text = author.Name;

            AuthorBooksList.ItemsSource = author.BooksWritten.ToList();
            AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private async void TxtAuthor_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            HyperlinkButton b = (HyperlinkButton)sender;
            var response = await AuthorData.GetBooksWithAuthor(Selected.Book.AuthorId);

            Author a = new Author()
            {
                Name = b.Content.ToString(),
                BooksWritten = response
            };
            PopulateAuthorDetails(a);
        }

        private void SearchBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            LightUpdateBooksList(SearchBox.Text);
        }

        private void LightUpdateBooksList(string search)
        {
            AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            List<Book> bookList = BookRequests;
            search = search.ToLower();

            bookList = bookList.Where(b => b.Title.ToLower().Contains(search)
            || b.Author.Name.ToLower().Contains(search)
            || b.Summary.ToLower().Contains(search)).ToList();

            BookRequestsList.ItemsSource = bookList;

        }

        private async void CheckInBook_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await BookData.TurnInBook(new BookRecordDto()
            {
                BookId = Selected.BookId,
                UserId = Selected.UserId
            });
        }

        private void CheckedOutBooksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            AuthorBooksList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            CheckInBook.Visibility = Windows.UI.Xaml.Visibility.Visible;

            try
            {
                var item = e.ClickedItem;

                Book book = (Book)item;

                Selected = AllBookRecords.Where(b => b.BookId == book.Id).FirstOrDefault();

                PopulateBookDetails(book);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
