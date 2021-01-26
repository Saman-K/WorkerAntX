using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace WorkerAntX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
        // Shows the main Window
        private async void GoBack_ClickAsync(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Opens SamanK.me website
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Website_Click(object sender, EventArgs e)
        {
            await Launcher.OpenAsync(new Uri("https://samank.me/Code"));
        }

        /// <summary>
        /// Opens github website
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Github_click(object sender, EventArgs e)
        {
            await Launcher.OpenAsync(new Uri("https://github.com/Saman-K"));
        }
    }
}