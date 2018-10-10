using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace ReCircle.Classes.http
{
    class AuthorData : BaseHttp
    {
        public static async Task<List<Author>> GetAuthors()
        {
            return await Get<List<Author>>("api/authors");
        }

        public static async Task<Author> GetAuthor(int Id)
        {
            return await Get<Author>($"api/authors/{Id}");
            
        }

        public static async Task<List<Book>> GetBooksWithAuthor(int authorId)
        {
            return await Get<List<Book>>($"api/authors/{authorId}/books");
        }
        
        public static async Task<HttpResponseMessage> PostAuthor(Author author)
        {
            return await Post<Author>("api/authors", author);
        }
    }
}
