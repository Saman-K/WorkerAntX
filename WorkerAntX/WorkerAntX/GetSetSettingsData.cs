//using System;
//using System.Runtime.InteropServices;
//using Xamarin.Forms;
//using WorkerAntX.Views;

//namespace WorkerAntX
//{
//    public static class GetSetSettingsData
//    {

//        /// <summary>
//        /// It will save the settings.
//        /// </summary>
//        /// <param name="workRecovery">Lap Package:Recovery Segment:Work</param>
//        /// <param name="breakRecovery">Lap Package:Recovery Segment:Break</param>
//        /// <param name="workSmart">Lap Package:Smart Segment:Work</param>
//        /// <param name="breakSmart">Lap Package:Smart Segment:Break</param>
//        /// <param name="workProgress">Lap Package:Progress Segment:Work</param>
//        /// <param name="breakProgress">Lap Package:Progress Segment:Break</param>
//        /// <param name="lapCounter">Segment:Lap Counter</param>
//        /// <param name="audioAlert">Audio Alert</param>
//        /// <param name="simpleUI">Simple UI</param>
//        /// <param name="autoStart">Auto Start</param>
//        /// <returns>String: User readable message.</returns>
//        public static string SaveSettings(decimal workRecovery, decimal breakRecovery, decimal workSmart, decimal breakSmart, decimal workProgress,
//            decimal breakProgress, decimal lapCounter, bool audioAlert, bool simpleUI, bool autoStart, bool breakInfo)
//        {
//            var message = "";
//            try
//            {
//                Settings.RecoveryWorkTime = workRecovery * 60;
//                Settings.RecoveryBreakTime = breakRecovery * 60;
//                Settings.SmartWorkTime = workSmart * 60;
//                Settings.SmartBreakTime = breakSmart * 60;
//                Settings.ProgressWorkTime = workProgress * 60;
//                Settings.ProgressBreakTime = breakProgress * 60;
//                Settings.LapCounter = lapCounter;

//                Properties.Settings.Default.audioAlert = audioAlert;
//                Properties.Settings.Default.breakStretches = breakInfo;

//                if (Properties.Settings.Default.simpleUI != simpleUI)
//                {
//                    Properties.Settings.Default.simpleUI = simpleUI;
//                    message = "Re-lunch the page to see the change. ";

//                    //notify balloon can be used
//                }

//                Properties.Settings.Default.Save();
//                message += "Saved.";
//            }
//            catch
//            {
//                message = "Settings could not be saved!";
//            }
//            return message;
//        }


//        /// <summary>
//        /// Get user preferred working settings.
//        /// </summary>
//        /// <returns>Audio alert, Simple UI, Auto Start</returns>
//        public static (bool audioAlert, bool simpleUI, bool autoStart, bool breakInfo)
//            GetSettingsUserPreferences()
//        {
//            bool audioAlert;
//            bool simpleUI;
//            bool autoStart;
//            bool breakInfo;

//            audioAlert = Properties.Settings.Default.audioAlert;
//            simpleUI = Properties.Settings.Default.simpleUI;
//            breakInfo = Properties.Settings.Default.breakStretches;

//            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);

//            if (registry.GetValue(Application.ProductName) != null && Application.ExecutablePath == registry.GetValue(Application.ProductName).ToString())
//            {
//                autoStart = true;
//            }
//            else
//            {
//                autoStart = false;
//            }

//            return (audioAlert, simpleUI, autoStart, breakInfo);
//        }
//    }
//}
