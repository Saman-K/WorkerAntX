using System;
using WorkerAntX.Views;
using Xamarin.Forms;

namespace WorkerAntX
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        #region 
        // Opens Settings page 
        private async void BtnSettings_ClickAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SettingsPage());
        }

        // Opens About page 
        private async void BtnAbout_ClickAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AboutPage());
        }
        #endregion

        #region
        // Set button click
        private void SetBtn(object sender, EventArgs e)
        {

        }

        // Start/Stop button click
        private void StartStopBtn(object sender, EventArgs e)
        {

        }

        //Radio button check changed
        private void RadioBtnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            GetLapPackage();


        }

        // Get radio btn selected lap package
        private void GetLapPackage()
        {
            if (radioBtnManual.IsChecked == false)
            {

            }

            if (radioBtnManual.IsChecked == true)
            {

            }
            else if (radioBtnRecovery.IsChecked == true)
            {

            }
            else if (radioBtnSmart.IsChecked == true)
            {

            }
            else if (radioBtnProgress.IsChecked == true)
            {

            }
            else
            {

            }
        }
        #endregion
    }
}
