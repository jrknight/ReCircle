using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCircle.Classes.http.dto
{
    public class ItemForCreationDto
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string Summary { get; set; }
        public string ISBN { get; set; }

        public ArrayList GenreIds = new ArrayList();
    }
}
