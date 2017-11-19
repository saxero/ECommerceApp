using ShoppingCart.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingCart
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}
