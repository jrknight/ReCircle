using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReCircle.Classes
{
    public class ItemRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public Item Item { get; set; }
        
        /*[Required]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public User Owner { get; set; }*/
        public string OwnerEmail { get; set; }
        public string OwnerNickname { get; set; }

        public string CurrentHolderEmail { get; set; }
        public string CurrentHolderNickname { get; set; }

        public string RequestMessage { get; set; }

        public DateTime RequestDate { get; set; }

    }
}
