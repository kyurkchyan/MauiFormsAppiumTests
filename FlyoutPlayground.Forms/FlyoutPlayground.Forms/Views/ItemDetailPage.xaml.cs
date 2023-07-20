using System.ComponentModel;
using Xamarin.Forms;
using FlyoutPlayground.Forms.ViewModels;

namespace FlyoutPlayground.Forms.Views
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
