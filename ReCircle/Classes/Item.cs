using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReCircle.Classes
{
    [Table("tblItem")]
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Uri PhotoUrl { get; set; }
        [Required]
        public string OwnerEmail { get; set; }
        public string OwnerUsername { get; set; }
        [Required]
        public string CurrentHolderEmail { get; set; }
        public string CurrentHolderUsername { get; set; }


        
        public ICollection<ItemRequest> ItemRequests { get; set; }
        public ICollection<ItemRecord> ItemRecords { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
