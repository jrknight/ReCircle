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
    class ItemData : BaseHttp
    {

        public static async Task<List<Item>> GetItems()
        {
            var data = await Get<List<Item>>("api/books");
            Debug.WriteLine(data.ToString());
            //HttpResponseMessage h = await BaseHttp.GetLibThing<Book>();
            return data;
        }

        public static async Task<Item> GetItem(int Id)
        {
            return await Get<Item>($"api/books/{Id}");
        }

        public static async Task<HttpResponseMessage> PostItem(BookForCreationDto book)
        {
            return await Post("api/books", book);
        }

        public static async Task<List<ItemRequest>> GetUsersItemRequests()
        {
            return await Get<List<ItemRequest>>($"api/books/requests/user/");
        }

        public static async Task<List<ItemRecord>> GetCheckedOutItems()
        {
            return await Get<List<ItemRecord>>($"api/books/records/users/");
        }

        public static async Task<List<ItemRecord>> GetAllCheckedOutItems()
        {
            return await Get<List<ItemRecord>>("api/books/records");
        }

        public static async Task<HttpResponseMessage> PostNewItemRequest(BookRequestDto request)
        {
            return await Post("api/books/requests", request);
        }

        public static async Task<HttpResponseMessage> PostItemCheckOut(BookRecordDto record)
        {
            return await Post("api/books/records", record);
        }

        public static async Task<List<ItemRequest>> GetAllItemRequests()
        {
            return await Get<List<ItemRequest>>("api/books/requests");
        }

        public static async Task<HttpResponseMessage> TurnInItem(BookRecordDto bookRecord)
        {
            return await Delete("api/books/records", bookRecord);
        }

    }
}
