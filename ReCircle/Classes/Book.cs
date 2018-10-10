using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReCircle.Classes
{
    [XmlRoot(DataType = "string", ElementName = "response")]
    public class Book
    {

        public int Id { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<BookGenre> BookGenres { get; set; }
        public string Summary { get; set; }
        public string ISBN { get; set; }
         
        public Book()
        {
            return;
        }

        public Book(string Title, int AuthorId, string Genre, string Summary, string ISBN)
        {
            this.Title = Title;
            this.AuthorId = AuthorId;
            this.Summary = Summary;
            this.ISBN = ISBN;
        }

        
    }
}
