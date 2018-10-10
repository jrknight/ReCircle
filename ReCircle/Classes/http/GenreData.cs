using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace ReCircle.Classes.http
{
    class GenreData : BaseHttp
    {
        public static async Task<List<Genre>> GetGenres()
        {
            return await Get<List<Genre>>("api/books/genres");
        }

        public static async Task<HttpResponseMessage> PostGenre(Genre genre)
        {
            return await Post<Genre>("api/books/genres", genre);
        }
    }
}
