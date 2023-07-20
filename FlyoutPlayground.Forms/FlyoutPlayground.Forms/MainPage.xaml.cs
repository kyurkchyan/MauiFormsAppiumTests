using System;
using Xamarin.Forms;

namespace FlyoutPlayground.Forms
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            Detail = new NavigationPage(new Page1());
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            switch(button.Text)
            {
                case "Page1":
                {
                    Detail = new NavigationPage(new Page1());
                    break;
                }
                case "Page2":
                {
                    Detail = new NavigationPage(new Page2());
                    break;
                }
            }
        }
    }
}