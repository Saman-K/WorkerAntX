using System;
using WorkerAntX.Views;
using Xamarin.Forms;

namespace WorkerAntX
{
    public partial class MainPage : ContentPage
    {
        #region Properties and Field

        SettingsPage settingsPage = new SettingsPage();

        private (int Work, int Break, int Laps) PreviewLapPackage { get; set; }

        #endregion

        public MainPage()
        {
            InitializeComponent();

            Countdown.Initialization();

            LiveUpdate();

            SetBtnClick(null, null);
            //Countdown.CounterTickEvent += UpdateTimer;

            //UpdateTimer(null, null);
        }

        #region Methods
        /// <summary>
        /// Opens Settings page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnSettings_ClickAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(settingsPage);
        }

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
        /// Set button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetBtnClick(object sender, EventArgs e)
        {
            if (Settings.LastUsedLapPackage == (int)LapPackageNames.Recovery)
            {
                PreviewLapPackage = LapPackageNames.Recovery.GetLapPackageValue();
                LabelLapPackageNames.Text = "Recovery";
                MainBackground.BackgroundColor = Color.FromHex("#00a787");
            }
            else if (Settings.LastUsedLapPackage == (int)LapPackageNames.Balance)
            {
                PreviewLapPackage = LapPackageNames.Balance.GetLapPackageValue();
                LabelLapPackageNames.Text = "Balance";
                MainBackground.BackgroundColor = Color.FromHex("#0082c1");
            }
            else if (Settings.LastUsedLapPackage == (int)LapPackageNames.Progress)
            {
                PreviewLapPackage = LapPackageNames.Progress.GetLapPackageValue();
                LabelLapPackageNames.Text = "Progress";
                MainBackground.BackgroundColor = Color.FromHex("#d95252");

            }
            else
            {
                //MessageBox.Show("Radio Button not found!", "WorkerAnt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PreviewLapPackage = LapPackageNames.Balance.GetLapPackageValue();
                LabelLapPackageNames.Text = "!";
            }

            Countdown.LastUserInput = PreviewLapPackage;

            //ProgressBarCountdown.Value = Countdown.GetProgressInPercentage(SegmentNames.Paused);

            SetBtn.Text = "Reset";
            Countdown.Set();
        }

        /// <summary>
        /// Start/Stop button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartStopBtnClick(object sender, EventArgs e)
        {
            if (StartStopBtn.Text == "Start")
            {
                if (LabelWorkTimeCountdown.Text == "0:00" || LabelBreakTimeCountdown.Text == "0:00")
                {
                    SetBtnClick(null, null);
                }

                SetBtn.IsEnabled = false;
            }
            else if (StartStopBtn.Text == "Stop")
            {
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

        /// <summary>
        /// Updates The UI
        /// </summary>
        public void LiveUpdate()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            {
                LabelWorkTimeCountdown.Text = Countdown.WorkTimerLive.IntToTimerFormat();

                LabelBreakTimeCountdown.Text = Countdown.BreakTimerLive.IntToTimerFormat();

                LabelLapCountdown.Text = Countdown.LapCounterLive.ToString();

                if (Countdown.TimeTickSegment == SegmentNames.Work)
                {
                    ProgressBarCountdown.Progress = Countdown.GetProgressInPercentage(SegmentNames.Work);
                    ProgressBarCountdown.ProgressColor = Color.FromHex("#222222");
                    LabelBreakTimeCountdown.TextColor = LabelWorkTimeCountdown.TextColor;
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
                    LabelBreakTimeCountdown.Text = "- " + Countdown.BreakTimerLive.IntToTimerFormat();
                    LabelBreakTimeCountdown.TextColor = Color.FromHex("#901E26");
                }
                else
                {
                    ProgressBarCountdown.Progress = Countdown.GetProgressInPercentage(SegmentNames.Paused);
                    LabelBreakTimeCountdown.TextColor = LabelWorkTimeCountdown.TextColor;
                }

                if (Countdown.TimerTick == true)
                {
                    StartStopBtn.Text = "Stop";
                    SetBtn.IsEnabled = false;
                }
                else if (Countdown.TimerTick == false)
                {
                    StartStopBtn.Text = "Start";
                    SetBtn.IsEnabled = true;
                }

                return true;
            });
        }
        #endregion
    }
}
