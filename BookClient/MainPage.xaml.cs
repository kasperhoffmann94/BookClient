using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClient.Data;
using Xamarin.Forms;

namespace BookClient
{
    public partial class MainPage : ContentPage
    {
        private BookManager odManager = new BookManager();
        public MainPage()
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<BookItem> bookCollection = await odManager.GetAllBooks();
            bookListView.ItemsSource = bookCollection;
        }

        private async void OnAddBookClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddBook());
        }

        private void BookListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var currentBook = (sender as ListView).SelectedItem as BookItem;
            Navigation.PushAsync(new BookDetailPage(currentBook));
        }
    }
}
