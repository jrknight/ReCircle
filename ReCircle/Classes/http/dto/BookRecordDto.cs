using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCircle.Classes.http.dto
{
    class BookRecordDto
    {
        public int BookId { get; set; }
        public DateTime RequestDate { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
