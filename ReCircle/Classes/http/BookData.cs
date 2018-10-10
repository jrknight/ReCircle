using ReCircle.Classes.http.dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace ReCircle.Classes.http
{
    class BookData : BaseHttp
    {

        public static async Task<List<Book>> GetBooks()
        {
            var data = await Get<List<Book>>("api/books");
            Debug.WriteLine(data.ToString());
            //HttpResponseMessage h = await BaseHttp.GetLibThing<Book>();
            return data;
        }

        public static async Task<Book> GetBook(int Id)
        {
            return await Get<Book>($"api/books/{Id}");
        }

        public static async Task<HttpResponseMessage> PostBook(BookForCreationDto book)
        {
            return await Post("api/books", book);
        }

        public static async Task<List<BookRequest>> GetUsersBookRequests()
        {
            return await Get<List<BookRequest>>($"api/books/requests/user/");
        }

        public static async Task<List<BookRecord>> GetCheckedOutBooks()
        {
            return await Get<List<BookRecord>>($"api/books/records/users/");
        }

        public static async Task<List<BookRecord>> GetAllCheckedOutBooks()
        {
            return await Get<List<BookRecord>>("api/books/records");
        }

        public static async Task<HttpResponseMessage> PostNewBookRequest(BookRequestDto request)
        {
            return await Post("api/books/requests", request);
        }

        public static async Task<HttpResponseMessage> PostBookCheckOut(BookRecordDto record)
        {
            return await Post("api/books/records", record);
        }

        public static async Task<List<BookRequest>> GetAllBookRequests()
        {
            return await Get<List<BookRequest>>("api/books/requests");
        }

        public static async Task<HttpResponseMessage> TurnInBook(BookRecordDto bookRecord)
        {
            return await Delete("api/books/records", bookRecord);
        }

    }
}
