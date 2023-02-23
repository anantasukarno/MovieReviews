using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
[assembly: ExportFont("PoppinsMedium.ttf", Alias = "Poppins")]
[assembly: ExportFont("PoppinsBold.ttf", Alias = "PoppinsBold")]
[assembly: ExportFont("PoppinsBlack.ttf", Alias = "PoppinsBlack")]
[assembly: ExportFont("PoppinsExtraBold.ttf", Alias = "PoppinsExtraBold")]
[assembly: ExportFont("PoppinsMediumRegular.ttf", Alias = "PoppinsRegular")]

namespace MoviesApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Genre());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
