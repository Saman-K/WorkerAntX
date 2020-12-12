using System;
using WorkerAntX.Views;
using Xamarin.Forms;

namespace WorkerAntX
{
    public partial class MainPage : ContentPage
    {
        #region Properties

        //int numbertest;

        //private Timer testtimer = new Timer(10000);

        // saving data for preview
        private (int Work, int Break, int Laps) PreviewLapPackage { get; set; }
        #endregion

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

            //BindingContext = new MainPageViewModel();
            LiveUpdate();


            Countdown.Initialization();

            ((LabelRecoveryWorkTimePreview.Text, LabelRecoveryBreakTimePreview.Text), (LabelSmartWorkTimePreview.Text, LabelSmartBreakTimePreview.Text), (LabelProgressWorkTimePreview.Text,
                LabelProgressBreakTimePreview.Text), (LabelManualWorkTimePreview.Text, LabelManualBreakTimePreview.Text), LabelLapCounter.Text) = Settings.GetSattingsLapPackages();

            LapCounterStepper.Value = Settings.LapCounter;
            StepperManualWorkTime.Value = Settings.ManualWorkTime;
            StepperManualBreakTime.Value = Settings.ManualBreakTime;

            LabelManualWorkTimePreview.BindingContext = StepperManualWorkTime.Value;
            LabelManualBreakTimePreview.BindingContext = StepperManualBreakTime.Value;
            LabelLapCounter.BindingContext = LapCounterStepper.Value;
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

        #region Methods
        // Set button click
        private void SetBtnClick(object sender, EventArgs e)
        {
            if (RadioBtnRecovery.IsChecked == true)
            {
                PreviewLapPackage = LapPackageNames.Recovery.GetLapPackageValue();
            }
            else if (RadioBtnSmart.IsChecked == true)
            {
                PreviewLapPackage = LapPackageNames.Smart.GetLapPackageValue();
            }
            else if (RadioBtnProgress.IsChecked == true)
            {
                PreviewLapPackage = LapPackageNames.Progress.GetLapPackageValue();
            }
            else if (RadioBtnManual.IsChecked == true)
            {
                PreviewLapPackage = LapPackageNames.Manual.GetLapPackageValue();
            }
            else
            {
                //MessageBox.Show("Radio Button not found!", "WorkerAnt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PreviewLapPackage = LapPackageNames.Smart.GetLapPackageValue();
            }

            Countdown.LastUserInput = PreviewLapPackage;

            //ProgressBarCountdown.Value = Countdown.GetProgressInPercentage(SegmentNames.Paused);

            SetBtn.Text = "Reset";
            Countdown.Set();

            //SaveLapPackageUsed();
        }
        // Start/Stop button click
        private void StartStopBtnClick(object sender, EventArgs e)
        {
            if (StartStopBtn.Text == "Start")
            {
                if (LabelWorkTimeCountdown.Text == "0:00" || LabelBreakTimeCountdown.Text == "0:00")
                {
                    SetBtnClick(null, null);
                }
                RadioBtnManual.IsEnabled = false;
                StepperManualWorkTime.IsEnabled = false;
                StepperManualBreakTime.IsEnabled = false;
                RadioBtnRecovery.IsEnabled = false;
                RadioBtnSmart.IsEnabled = false;
                RadioBtnProgress.IsEnabled = false;

                LapCounterStepper.IsEnabled = false;
                SetBtn.IsEnabled = false;
            }
            else if (StartStopBtn.Text == "Stop")
            {
                RadioBtnManual.IsEnabled = true;
                if (RadioBtnManual.IsChecked == false)
                {
                    StepperManualWorkTime.IsEnabled = false;
                    StepperManualBreakTime.IsEnabled = false;
                }
                else
                {
                    StepperManualWorkTime.IsEnabled = true;
                    StepperManualBreakTime.IsEnabled = true;
                }

                RadioBtnRecovery.IsEnabled = true;
                RadioBtnSmart.IsEnabled = true;
                RadioBtnProgress.IsEnabled = true;

                LapCounterStepper.IsEnabled = true;
                SetBtn.IsEnabled = true;
            }

            try
            {
                StartStopBtn.Text = StartStopBtn.Text.StartStop();
            }
            catch (TimeoutException ex)
            {
                DisplayAlert("WorkerAnt", ex.Message, "OK");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                DisplayAlert("WorkerAnt", ex.Message, "OK");
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
                StepperManualWorkTime.IsEnabled = true;
                StepperManualBreakTime.IsEnabled = true;
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
            LabelManualWorkTimePreview.Text = "Work:    " + ((int)e.NewValue).IntToTimerFormat();
        }

        /// <summary>
        /// Manual break stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void ManualBreakTimeStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.ManualBreakTime = (int)e.NewValue;
            LabelManualBreakTimePreview.Text = "Break:    " + ((int)e.NewValue).IntToTimerFormat();

        }

        /// <summary>
        /// Lap counter stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void LapCounterStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.LapCounter = (int)e.NewValue;
            LabelLapCounter.Text = "Laps:    " + ((int)e.NewValue).ToString();
        }

        /// <summary>
        /// Updates The UI
        /// </summary>
        public void LiveUpdate()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            {
                LabelWorkTimeCountdown.Text = Countdown.WorkTimerLive.IntToTimerFormat();

                if (Countdown.TimeTickSegment == SegmentNames.EndBreak)
                {
                    LabelBreakTimeCountdown.Text = "- " + Countdown.BreakTimerLive.IntToTimerFormat();
                    LabelBreakTimeCountdown.TextColor = Color.FromHex("#901E26");
                }
                else
                {
                    LabelBreakTimeCountdown.Text = Countdown.BreakTimerLive.IntToTimerFormat();
                }

                LabelLapCountdown.Text = Countdown.LapCounterLive.ToString();

                if (Countdown.TimerTick == true)
                {
                    StartStopBtn.Text = "Stop";
                    SetBtn.IsEnabled = false;

                    RadioBtnManual.IsEnabled = false;
                    StepperManualWorkTime.IsEnabled = false;
                    StepperManualBreakTime.IsEnabled = false;
                    RadioBtnRecovery.IsEnabled = false;
                    RadioBtnSmart.IsEnabled = false;
                    RadioBtnProgress.IsEnabled = false;

                    LapCounterStepper.IsEnabled = false;
                }
                else if (Countdown.TimerTick == false)
                {
                    StartStopBtn.Text = "Start";
                    SetBtn.IsEnabled = true;

                    RadioBtnManual.IsEnabled = true;
                    if (RadioBtnManual.IsChecked == false)
                    {
                        StepperManualWorkTime.IsEnabled = false;
                        StepperManualBreakTime.IsEnabled = false;
                    }
                    else
                    {
                        StepperManualWorkTime.IsEnabled = true;
                        StepperManualBreakTime.IsEnabled = true;
                    }

                    RadioBtnRecovery.IsEnabled = true;
                    RadioBtnSmart.IsEnabled = true;
                    RadioBtnProgress.IsEnabled = true;

                    LapCounterStepper.IsEnabled = true;
                }

                if (Countdown.TimeTickSegment == SegmentNames.Work)
                {
                    ProgressBarCountdown.Progress = Countdown.GetProgressInPercentage(SegmentNames.Work);
                    ProgressBarCountdown.ProgressColor = Color.FromHex("#222222");
                }
                else if (Countdown.TimeTickSegment == SegmentNames.Break)
                {
                    ProgressBarCountdown.Progress = Countdown.GetProgressInPercentage(SegmentNames.Break);
                    ProgressBarCountdown.ProgressColor = Color.FromHex("#407294");
                }
                else if (Countdown.TimeTickSegment == SegmentNames.EndBreak)
                {
                    ProgressBarCountdown.Progress = 1;
                    ProgressBarCountdown.ProgressColor = Color.FromHex("#901E26");
                }
                else
                {
                    ProgressBarCountdown.Progress = Countdown.GetProgressInPercentage(SegmentNames.Paused);
                }

                return true;
            });
        }
        #endregion


        private void testBtn_Clicked(object sender, EventArgs e)
        {
            LabelWorkTimeCountdown.Text = Countdown.WorkTimerLive.IntToTimerFormat();
            LabelBreakTimeCountdown.Text = Countdown.BreakTimerLive.IntToTimerFormat();
            LabelLapCountdown.Text = Countdown.LapCounterLive.ToString();


        }
    }
}
