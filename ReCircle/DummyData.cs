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
            Instance = new DummyData { BrowsePage = new List<Item> { new Item { Title = "Item", OwnerEmail="elijahb@gmail", OwnerNickname = "Elijah", Description = "Like new", CurrentHolderNickname = "Elijah", PhotoUrl = new Uri("https://media03.toms.com/static/www/images/product/MENS/ATG/SIDE/10011588-GreyLinenMensPreston-P-1450x1015.jpg") },
            new Item{ Title = "Shoes", OwnerEmail="coltonj@gmail", OwnerNickname= "Colton", Description = "Used, no use for them now", CurrentHolderNickname = "Colton", PhotoUrl = new Uri("https://cdn.shopify.com/s/files/1/0898/5824/products/QUAY_HIGHKEY_BLACK_FADE_FRONT_1024x1024.jpg?v=1537995892") },
            new Item { Title = "Sunglasses", OwnerEmail="denielleo@gmail", OwnerNickname = "Denielle", Description = "No scratches, polarized", CurrentHolderNickname = "Elijah", PhotoUrl = new Uri("https://cdn.shopify.com/s/files/1/0898/5824/products/QUAY_HIGHKEY_BLACK_FADE_FRONT_1024x1024.jpg?v=1537995892") },
            new Item { Title = "Hat", OwnerEmail="joshk@gmail", OwnerNickname = "Josh", Description = "Looks fine, works like a hat", CurrentHolderNickname = "Josh", PhotoUrl = new Uri("https://media03.toms.com/static/www/images/product/MENS/ATG/SIDE/10011588-GreyLinenMensPreston-P-1450x1015.jpg") },
            new Item { Title = "Coffee Mug", OwnerEmail="denielleo@gmail.com", OwnerNickname = "Denielle", Description = "Its a coffee mug", CurrentHolderNickname = "Dave! Yognaught", PhotoUrl = new Uri("https://cdn.shopify.com/s/files/1/0898/5824/products/QUAY_HIGHKEY_BLACK_FADE_FRONT_1024x1024.jpg?v=1537995892") },
            new Item { Title = "HydroFlask", OwnerEmail="joshk@gmail", OwnerNickname = "Josh", Description = "Like new", CurrentHolderNickname = "Elijah", PhotoUrl = new Uri("https://cdn.shopify.com/s/files/1/0898/5824/products/QUAY_HIGHKEY_BLACK_FADE_FRONT_1024x1024.jpg?v=1537995892") } } };
        }

        public List<Item> BrowsePage = new List<Item>();
        public List<ItemRequest> RequestedItems = new List<ItemRequest>();

        public static void RequestItem(Item item)
        {

        }

    }

}
