using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace WorkerAntX.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {

        public string WorkTimer
        {
            get
            {
                return Countdown.WorkTimerLive.IntToTimerFormat();
            }
        }
        public string BreakTimer
        {
            get
            {
                return Countdown.BreakTimerLive.IntToTimerFormat();
            }
        }
        public string LapCounter
        {
            get
            {
                return Countdown.LapCounterLive.ToString();
            }
        }


        public MainPageViewModel()
        {
            Device.StartTimer(TimeSpan.FromSeconds(0), () =>
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("WorkTimer"));
                    PropertyChanged(this, new PropertyChangedEventArgs("BreakTimer"));
                    PropertyChanged(this, new PropertyChangedEventArgs("LapCounter"));

                }
                return true;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
