using AppStock.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppStock.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}