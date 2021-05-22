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
    public partial class AddBook : ContentPage
    {
        private BookManager bookManager = new BookManager();
        public AddBook()
        {
            InitializeComponent();
        }

        private async void AddBookButtonClicked(object sender, EventArgs e)
        {
            await bookManager.Add(TitleEntry.Text, DescriptionEntry.Text, ISBNEntry.Text);
            await Navigation.PushAsync(new MainPage());
        }
    }
}