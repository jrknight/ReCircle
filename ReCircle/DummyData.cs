using ReCircle.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCircle
{
    public class DummyData
    {
        public static DummyData Instance { get; set; }

        public static void Setup()
        {
            Instance = new DummyData { BrowsePage = new List<Item> { new Item { Title = "Item", OwnerNickname = "Josh", Description = "Like new", CurrentHolderNickname = "Elijah", PhotoUrl = "photos.google.com" },
            new Item{ Title = "Shoes", OwnerNickname= "Colton", Description = "Used, no use for them now", CurrentHolderNickname = "Colton", PhotoUrl = "photos.google.com" },
            new Item { Title = "Sunglasses", OwnerNickname = "Denielle", Description = "No scratches, polarized", CurrentHolderNickname = "Elijah", PhotoUrl = "photos.google.com" },
            new Item { Title = "Hat", OwnerNickname = "Josh", Description = "Looks fine, works like a hat", CurrentHolderNickname = "Josh", PhotoUrl = "photos.google.com" },
            new Item { Title = "Coffee Mug", OwnerNickname = "Denielle", Description = "Its a coffee mug", CurrentHolderNickname = "Dave! Yognaught", PhotoUrl = "photos.google.com" },
            new Item { Title = "HydroFlask", OwnerNickname = "Josh", Description = "Like new", CurrentHolderNickname = "Elijah", PhotoUrl = "photos.google.com" } } };
        }

        public List<Item> BrowsePage = new List<Item>();
        public List<ItemRequest> RequestedItems = new List<ItemRequest>();

        public static void RequestItem(Item item)
        {

        }

    }

}
