using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClient.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailPage : ContentPage
    {
        private BookItem book;
        private BookManager bookManager = new BookManager();
        public BookDetailPage(BookItem currentBook)
        {
            InitializeComponent();
            book = currentBook;
        }

        protected override void OnAppearing()
        {
            TitleLabel.Text = book.Title;
            DescriptionLabel.Text = book.Description;
            ISBNLabel.Text = book.ISBN;
        }

        private async void DeleteBookButtonClicked(object sender, EventArgs e)
        {
            bookManager.Delete(book.id);
            await Navigation.PushAsync(new MainPage());
        }

        private async void UpdateBookButtonClicked(object sender, EventArgs e)
        {
            await bookManager.Update(TitleLabel.Text, DescriptionLabel.Text, ISBNLabel.Text, book.id);
            await Navigation.PushAsync(new MainPage());
        }
    }
}