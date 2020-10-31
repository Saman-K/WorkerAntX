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

            if (Settings.LastUsedLapPackage == (int)LapPackageNames.Manual)
            {
                RadioBtnManual.IsChecked = true;
            }
            else if (Settings.LastUsedLapPackage == (int)LapPackageNames.Recovery)
            {
                RadioBtnRecovery.IsChecked = true;
            }
            else if (Settings.LastUsedLapPackage == (int)LapPackageNames.Smart)
            {
                RadioBtnSmart.IsChecked = true;
            }
            else if (Settings.LastUsedLapPackage == (int)LapPackageNames.Progress)
            {
                RadioBtnProgress.IsChecked = true;
            }
            else
            {
                // error "Radio Button not found!"
                RadioBtnSmart.IsChecked = true;
            }

            ((labelRecoveryWorkTimePreview.Text, labelRecoveryBreakTimePreview.Text), (labelSmartWorkTimePreview.Text, labelSmartBreakTimePreview.Text), (labelProgressWorkTimePreview.Text,
                labelProgressBreakTimePreview.Text), (LabelManualWorkTimePreview.Text, LabelManualBreakTimePreview.Text), LapCounter.Text) = Settings.GetSattingsLapPackages();

            LapCounterStepper.Value = Settings.LapCounter;
            StepperManualWorkTime.Value = Settings.ManualWorkTime;
            StepperManualBreakTime.Value = Settings.ManualBreakTime;

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
        private void SetBtnClick(object sender, EventArgs e)
        {
            //if (RadioBtnManual.IsChecked == true)
            //{
            //    Properties.Settings.Default.manualWorkTime = numUDWorkManual.Value * 60;
            //    Properties.Settings.Default.manualBreakTime = numUDBreakManual.Value * 60;
            //    Properties.Settings.Default.Save();
            //}

            //GetLapPackage();

            //labelWorkTimeCountdown.Text = PreviewLapPackage.Work.IntToTimerFormat();
            //labelBreakTimeCountdown.Text = PreviewLapPackage.Break.IntToTimerFormat();
            //labelLapCounterLive.Text = PreviewLapPackage.Laps.ToString();

            //progressBarCountdown.Value = Countdown.GetProgressInPercentage(SegmentNames.Paused);

            //Countdown.LastUserInput = PreviewLapPackage;
            //btnSetReset.Text = "Reset";
            //Countdown.Set();

            //SaveLapPackageUsed();
        }

        // Start/Stop button click
        private void StartStopBtnClick(object sender, EventArgs e)
        {
            if (StartStopBtn.Text == "Start")
            {
                if (LabelWorkTimeCountdown.Text == "Work Timer" || LabelBreakTimeCountdown.Text == "Break Timer")
                {
                    SetBtnClick(null, null);
                }
            //    liveDataUpdate.Start();
            //}
            //else if (StartStopBtn.Text == "Stop" && Countdown.TimeTickSegment == SegmentNames.Break)
            //{
            //    liveDataUpdate.Stop();
            //}

            //try
            //{
            //    StartStopBtn.Text = StartStopBtn.Text.StartStop();
            //}
            //catch (TimeoutException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //catch (ArgumentOutOfRangeException ex)
            //{
            //    MessageBox.Show(ex.Message, "WorkerAnt", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //Radio button check changed
        private void RadioBtnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (RadioBtnManual.IsChecked == false)
            {
                StepperManualWorkTime.IsEnabled = false;
                StepperManualBreakTime.IsEnabled = false;
            }
            else
            {
                
            }

            if (RadioBtnManual.IsChecked == true)
            {
                StepperManualWorkTime.IsEnabled = true;
                StepperManualBreakTime.IsEnabled = true;
                Settings.LastUsedLapPackage = (int)LapPackageNames.Manual;
            }
            else if (RadioBtnRecovery.IsChecked == true)
            {
                Settings.LastUsedLapPackage = (int)LapPackageNames.Recovery;
            }
            else if (RadioBtnSmart.IsChecked == true)
            {
                Settings.LastUsedLapPackage = (int)LapPackageNames.Smart;
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
        /// Manual work stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void ManualWorkTimeStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.ManualWorkTime = (int)e.NewValue;
            LabelManualWorkTimePreview.Text = ((int)e.NewValue).IntToTimerFormat();
        }

        /// <summary>
        /// Manual break stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void ManualBreakTimeStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.ManualBreakTime = (int)e.NewValue;
            LabelManualBreakTimePreview.Text = ((int)e.NewValue).IntToTimerFormat();
        }

        /// <summary>
        /// Lap counter stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void LapCounterStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.LapCounter = (int)e.NewValue;
            LapCounter.Text = Convert.ToString((int)e.NewValue);
        }
        #endregion
    }
}
