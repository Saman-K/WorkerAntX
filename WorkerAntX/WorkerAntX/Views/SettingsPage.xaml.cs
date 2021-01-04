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

        public event EventHandler SettingsUpdeted;

        #region Methods

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
        /// Get Settings data
        /// </summary>
        private void GetData()
        {
            StepperRecoveryWorkTime.Value = Settings.RecoveryWorkTime;
            StepperRecoveryBreakTime.Value = Settings.RecoveryBreakTime;
            StepperSmartWorkTime.Value = Settings.SmartWorkTime;
            StepperSmartBreakTime.Value = Settings.SmartBreakTime;
            StepperProgressWorkTime.Value = Settings.ProgressWorkTime;
            StepperProgressBreakTime.Value = Settings.ProgressBreakTime;
        }

        /// <summary>
        /// OnBackButtonPressed
        /// </summary>
        /// <returns> true: stop the back button</returns>
        protected override bool OnBackButtonPressed()
        {
            SettingsUpdeted?.Invoke(this, null);

            return base.OnBackButtonPressed();
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
        /// Smart work stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void StepperSmartWorkTimeValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.SmartWorkTime = (int)e.NewValue;
            LabelSmartWorkTime.Text = ((int)e.NewValue).IntToTimerFormat();
        }

        /// <summary>
        /// Smart break stepper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">stepper value</param>
        private void StepperSmartBreakTimeValueChanged(object sender, ValueChangedEventArgs e)
        {
            Settings.SmartBreakTime = (int)e.NewValue;
            LabelSmartBreakTime.Text = ((int)e.NewValue).IntToTimerFormat();
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

        #endregion

        #endregion
    }
}