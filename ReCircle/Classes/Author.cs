using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCircle.Classes
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<Book> BooksWritten { get; set; } = new List<Book>();
    }
}
