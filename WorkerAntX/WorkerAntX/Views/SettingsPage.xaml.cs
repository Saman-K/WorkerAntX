using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkerAntX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();


            GetData();
        }

        #region Methods
        /// <summary>
        /// Opens About page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnAbout_ClickAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AboutPage());
        }

        /// <summary>
        /// Default Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultBtn(object sender, EventArgs e)
        {
            Settings.SetSettingsToDefault();

            GetData();
        }

        /// <summary>
        /// Radio button check changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioBtnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (RadioBtnRecovery.IsChecked == true)
            {
                Settings.LastUsedLapPackage = (int)LapPackageNames.Recovery;
            }
            else if (RadioBtnBalance.IsChecked == true)
            {
                Settings.LastUsedLapPackage = (int)LapPackageNames.Balance;
            }
            else if (RadioBtnProgress.IsChecked == true)
            {
                Settings.LastUsedLapPackage = (int)LapPackageNames.Progress;
            }
            else
            {
                // error "Radio Button not found"
            }
        }

        /// <summary>
        /// Get Settings data
        /// </summary>
        private void GetData()
        {
            StepperRecoveryWorkTime.Value = Settings.RecoveryWorkTime;
            StepperRecoveryBreakTime.Value = Settings.RecoveryBreakTime;
            StepperBalanceWorkTime.Value = Settings.BalanceWorkTime;
            StepperBalanceBreakTime.Value = Settings.BalanceBreakTime;
            StepperProgressWorkTime.Value = Settings.ProgressWorkTime;
            StepperProgressBreakTime.Value = Settings.ProgressBreakTime;
            LapCounterStepper.Value = Settings.LapCounter;

            if (Settings.LastUsedLapPackage == (int)LapPackageNames.Recovery)
            {
                RadioBtnRecovery.IsChecked = true;
            }
            else if (Settings.LastUsedLapPackage == (int)LapPackageNames.Balance)
            {
                RadioBtnBalance.IsChecked = true;
            }
            else if (Settings.LastUsedLapPackage == (int)LapPackageNames.Progress)
            {
                RadioBtnProgress.IsChecked = true;
            }
            else
            {
                // error "Radio Button not found!"
                RadioBtnBalance.IsChecked = true;
            }
        }

        #region Steppers
        /// <summary>
        /// Recovery work stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void StepperRecoveryWorkTimeValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.RecoveryWorkTime = (int)e.NewValue;
            LabelRecoveryWorkTime.Text = ((int)e.NewValue).IntToTimerFormat();
        }

        /// <summary>
        /// Recovery break stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void StepperRecoveryBreakTimeValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.RecoveryBreakTime = (int)e.NewValue;
            LabelRecoveryBreakTime.Text = ((int)e.NewValue).IntToTimerFormat();
        }

        /// <summary>
        /// Balance work stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void StepperBalanceWorkTimeValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.BalanceWorkTime = (int)e.NewValue;
            LabelBalanceWorkTime.Text = ((int)e.NewValue).IntToTimerFormat();
        }

        /// <summary>
        /// Smart break stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void StepperBalanceBreakTimeValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.BalanceBreakTime = (int)e.NewValue;
            LabelBalanceBreakTime.Text = ((int)e.NewValue).IntToTimerFormat();
        }

        /// <summary>
        /// Progress work stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void StepperProgressWorkTimeValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.ProgressWorkTime = (int)e.NewValue;
            LabelProgressWorkTime.Text = ((int)e.NewValue).IntToTimerFormat();
        }

        /// <summary>
        /// Progress break stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void StepperProgressBreakTimeValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.ProgressBreakTime = (int)e.NewValue;
            LabelProgressBreakTime.Text = ((int)e.NewValue).IntToTimerFormat();
        }

        /// <summary>
        /// Lap counter stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void LapCounterStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.LapCounter = (int)e.NewValue;
            LabelLapCounter.Text = ((int)e.NewValue).ToString();
        }

        #endregion

        #endregion
    }
}