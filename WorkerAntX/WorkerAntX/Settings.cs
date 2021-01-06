using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using Xamarin.Forms.Markup;

namespace WorkerAntX
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        #region Recovery
        private const string _RecoveryWorkKey = "Recovery_Work_Key";
        private static readonly int _RecoveryWorkDefault = 1800;

        private const string _RecoveryBreakKey = "Recovery_Break_Key";
        private static readonly int _RecoveryBreakDefault = 120;
        #endregion

        #region Balance
        private const string _BalanceWorkKey = "Balance_Work_Key";
        private static readonly int _BalanceWorkDefault = 2400;

        private const string _BalanceBreakKey = "Balance_Break_Key";
        private static readonly int _BalanceBreakDefault = 240;
        #endregion

        #region Progress
        private const string _ProgressWorkKey = "Progress_Work_Key";
        private static readonly int _ProgressWorkDefault = 3300;

        private const string _ProgressBreakKey = "Progress_Break_Key";
        private static readonly int _ProgressBreakDefault = 300;
        #endregion

        private const string _LapCounterKey = "Lap_Counter_Key";
        private static readonly int _LapCounterDefault = 1;
        
        private const string _LastUsedLapPackageKey = "Last_Used_Lap_Package_Key";
        private static readonly int _LastUsedLapPackageDefault = (int)LapPackageNames.Balance;
        #endregion

        #region properties

        #region Recovery
        public static int RecoveryWorkTime
        {
            get
            {
                return AppSettings.GetValueOrDefault(_RecoveryWorkKey, _RecoveryWorkDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_RecoveryWorkKey, value);
            }
        }

        public static int RecoveryBreakTime
        {
            get
            {
                return AppSettings.GetValueOrDefault(_RecoveryBreakKey, _RecoveryBreakDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_RecoveryBreakKey, value);
            }
        }
        #endregion

        #region Balance
        public static int BalanceWorkTime
        {
            get
            {
                return AppSettings.GetValueOrDefault(_BalanceWorkKey, _BalanceWorkDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_BalanceWorkKey, value);
            }
        }

        public static int BalanceBreakTime
        {
            get
            {
                return AppSettings.GetValueOrDefault(_BalanceBreakKey, _BalanceBreakDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_BalanceBreakKey, value);
            }
        }
        #endregion

        #region Progress
        public static int ProgressWorkTime
        {
            get
            {
                return AppSettings.GetValueOrDefault(_ProgressWorkKey, _ProgressWorkDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_ProgressWorkKey, value);
            }
        }

        public static int ProgressBreakTime
        {
            get
            {
                return AppSettings.GetValueOrDefault(_ProgressBreakKey, _ProgressBreakDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_ProgressBreakKey, value);
            }
        }
        #endregion
        public static int LapCounter
        {
            get
            {
                return AppSettings.GetValueOrDefault(_LapCounterKey, _LapCounterDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_LapCounterKey, value);
            }
        }

        public static int LastUsedLapPackage
        {
            get
            {
                return AppSettings.GetValueOrDefault(_LastUsedLapPackageKey, _LastUsedLapPackageDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_LastUsedLapPackageKey, value);
            }
        }
        #endregion

        /// <summary>
        /// Set settings to developers default 
        /// </summary>
        public static void SetSettingsToDefault()
        {
            RecoveryWorkTime = 1800;
            RecoveryBreakTime = 120;
            BalanceWorkTime = 2400;
            BalanceBreakTime = 240;
            ProgressWorkTime = 3300;
            ProgressBreakTime = 300;
            LapCounter = 1;

            //Properties.Settings.Default.breakStretches = true;
        }

        /// <summary>
        /// Get lap Packages working settings.
        /// </summary>
        /// <returns>(W,B) Recovery, (W,B) Balance, (W,B) Progress, Lap Counter</returns>
        public static ((string workRecovery, string breakRecovery), (string workBalance, string breakBalance), (string workProgress, string breakProgress), string lapCounter)
            GetSattingsLapPackages()
        {
            string workRecovery = (RecoveryWorkTime.IntToTimerFormat()).ToString();
            string breakRecovery = (RecoveryBreakTime.IntToTimerFormat()).ToString();
            string workBalance = (BalanceWorkTime.IntToTimerFormat()).ToString();
            string breakBalance = (BalanceBreakTime.IntToTimerFormat()).ToString();
            string workProgress = (ProgressWorkTime.IntToTimerFormat()).ToString();
            string breakProgress = (ProgressBreakTime.IntToTimerFormat()).ToString();
            string lapCounter = (LapCounter).ToString();

            return ((workRecovery, breakRecovery), (workBalance, breakBalance), (workProgress, breakProgress), lapCounter);
        }

        #region ------------------------------------------------------------------------- Extended Methods 

        /// <summary>
        /// it convert int "300" to user readable time "5:00".
        /// </summary>
        /// <param name="timer">Timer in seconds.</param>
        /// <returns>Formated return "00:00".</returns>
        public static string IntToTimerFormat(this int timer)
        {
            var formated = (timer / 60 + ":" + (timer % 60).ToString("D2"));

            return formated;
        }

        /// <summary>
        /// Get Saved Data
        /// </summary>
        /// <param name="package"></param>
        /// <returns>Work timer, Break timer, Number of laps</returns>
        public static (int Work, int Break, int Laps) GetLapPackageValue(this LapPackageNames package)
        {
            int WorkTime = 0;
            int BreakTime = 0;
            switch (package)
            {
                case (LapPackageNames.Recovery):
                    WorkTime = Convert.ToInt16(Settings.RecoveryWorkTime);
                    BreakTime = Convert.ToInt16(Settings.RecoveryBreakTime);
                    break;
                case (LapPackageNames.Balance):
                    WorkTime = Convert.ToInt16(Settings.BalanceWorkTime);
                    BreakTime = Convert.ToInt16(Settings.BalanceBreakTime);
                    break;
                case (LapPackageNames.Progress):
                    WorkTime = Convert.ToInt16(Settings.ProgressWorkTime);
                    BreakTime = Convert.ToInt16(Settings.ProgressBreakTime);
                    break;
                default:

                    break;
            }

            return (WorkTime, BreakTime, Convert.ToInt32(Settings.LapCounter));
        }
        #endregion
    }
}
