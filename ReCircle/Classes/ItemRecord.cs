using ReCircle.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReCircle.Classes
{
    
    public class ItemRecord
    {
        
        public int Id { get; set; }

        
        public int ItemId { get; set; }

        
        public Item Item { get; set; }

        
        public string UserId { get; set; }

        
        public User User { get; set; }

        
        public User Owner { get; set; }

        public DateTime RecordDate { get; set; }

    }
}
