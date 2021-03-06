﻿using ReCircle.Classes;
using ReCircle.Classes.http;
using ReCircle.Classes.http.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ReCircle.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReserveItemPage : Page
    {
        private List<Item> Books = new List<Item>();
        private Item ItemToSubmit = new Item();

        public ReserveItemPage()
        {
            this.InitializeComponent();
            Init();
        }

        private async void Init()
        {
            Books = await ItemData.GetItems();
            BooksList.ItemsSource = Books;
        }

        private void BooksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem;

            ItemToSubmit = (Item)item;

        }

        private void LightUpdateBooksList(string search)
        {

            List<Item> bookList = Books;
            search = search.ToLower();

            bookList = bookList.Where(b => b.Title.ToLower().Contains(search)).ToList();

            BooksList.ItemsSource = bookList;

        }
        private void SearchBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            LightUpdateBooksList(SearchBox.Text);
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Update Dtos

            var response = await ItemData.PostItemCheckOut(
                new BookRecordDto()
                {
                    BookId = ItemToSubmit.Id,
                    RequestDate = DateTime.Now,
                    UserName = UserNameTextBox.Text
                });
        }
    }
}
