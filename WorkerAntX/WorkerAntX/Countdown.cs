using System;
using System.Timers;

namespace WorkerAntX
{
    public static class Countdown
    {
        #region Fields and Properties
        #region Fields
        /// <summary>
        /// live work timer data
        /// </summary>
        private static int _workTimerLive = 0;
        /// <summary>
        /// live lap Counter data
        /// </summary>
        private static int _lapCounterLive = 0;

        /// <summary>
        /// Countdown timer
        /// </summary>
        private static Timer _countdownTimer;

        #endregion

        #region Properties
        /// <summary>
        /// last user inputed data
        /// Item1 == Work timer , Item2 == Break timer , Item3 == Lap counter
        /// </summary>
        public static (int Work, int Break, int Laps) LastUserInput { get; set; }

        /// <summary>
        /// Live updating work timer in seconds
        /// </summary>
        public static int WorkTimerLive
        {
            get
            {
                return _workTimerLive;
            }

            set
            {
                if (value >= 0)
                {
                    _workTimerLive = value;
                }
                else
                {
                    _workTimerLive = 0;
                }
            }

        }

        /// <summary>
        /// Live updating break timer in seconds
        /// </summary>
        public static int BreakTimerLive { get; set; } = 0;

        /// <summary>
        /// Number of lap left (private setter)
        /// </summary>
        public static int LapCounterLive
        {
            get
            {
                return _lapCounterLive;
            }
            private set
            {
                if (value > 0)
                {
                    _lapCounterLive = value;
                }
                else
                {
                    _lapCounterLive = 0;
                }
            }
        }

        /// <summary>
        /// Count up to 100 for Work segment.
        /// Count down to 0 for Break segment.
        /// Shows only 100 for End Break segment.
        /// 0 for anything else.
        /// (100 = 100%, 63 = 63%)
        /// </summary>
        public static double GetProgressInPercentage(SegmentNames segment)
        {
            if (segment == SegmentNames.Work)
            {
                return 1 - (Convert.ToDouble(WorkTimerLive) / Convert.ToDouble(LastUserInput.Work));
            }
            else if (segment == SegmentNames.Break)
            {
                return Convert.ToDouble(BreakTimerLive) / Convert.ToDouble(LastUserInput.Break);
            }
            else if (segment == SegmentNames.EndBreak)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get statistic of the lap 
        /// </summary>
        public static SegmentNames TimeTickSegment { get; private set; }
        /// <summary>
        /// Get if the timer is ticking.
        /// </summary>
        public static bool TimerTick { get; private set; }

        #endregion
        #endregion

        #region Initialization

        //public static event EventHandler CounterTickEvent;
        public static void Initialization()
        {
            _countdownTimer = new Timer(1000);
            _countdownTimer.Elapsed += CountdownTimer_Tick;
        }
        #endregion

        #region Method

        /// <summary>
        /// Timer 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (TimeTickSegment == SegmentNames.Work)
            {
                if (WorkTimerLive > 0)
                {

                    WorkTimerLive--;

                    if (WorkTimerLive == 300)
                    {
                        TimerController("ToBreakPopup");
                    }
                }
                else if (WorkTimerLive <= 0)
                {
                    TimerController("Break");
                }
            }
            else if (TimeTickSegment == SegmentNames.Break)
            {
                BreakTimerLive--;

                if (BreakTimerLive == 0)
                {
                    TimerController("End Break");
                }
            }
            else if (TimeTickSegment == SegmentNames.EndBreak)
            {
                BreakTimerLive++;

                //Audio Alert
                //Console.Beep(800, 100);

                // Can add a limit to how long EndBreak would last
            }
        }

        /// <summary>
        /// This will control the CountdownTimer() 
        /// </summary>
        /// <param name="function">Function that the controller has to complete </param> 
        private static void TimerController(string function)
        {
            switch (function)
            {
                case "ToBreakPopup":
                    try
                    {
                        //(WindowNames.ToBreak);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        //MessageBox.Show(ex.Message, "WorkerAnt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //Audio Alert
                    //Console.Beep(1000, 500);

                    break;
                case "Break":
                    _countdownTimer.Stop();
                    TimerTick = false;
                    //Audio Alert
                    //Console.Beep(1000, 500);

                    //try
                    //{
                    //    //(WindowNames.Break);
                    //}
                    //catch (ArgumentOutOfRangeException ex)
                    //{
                    //    //MessageBox.Show(ex.Message, "WorkerAnt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    TimeTickSegment = SegmentNames.Break;
                    _countdownTimer.Start();
                    TimerTick = true;
                    break;
                case "End Break":
                    TimeTickSegment = SegmentNames.EndBreak;
                    try
                    {
                        //(WindowNames.Main);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        //MessageBox.Show(ex.Message, "WorkerAnt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //Audio Alert
                    //Console.Beep(1000, 500);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException("function", function, "Please report to the developer (CD 241)");
            }
        }

        /// <summary>
        /// Start and stop the timer.
        /// The input has to be "Start" or "Stop".
        /// </summary>
        /// <param name="btnText">Takes the text of the button and execute the faction</param>
        /// <returns>The name of the button after execution</returns>
        public static string StartStop(this string btnText)
        {
            //Start timer
            if (btnText == "Start")
            {
                //start from the top
                if ((WorkTimerLive == LastUserInput.Work || BreakTimerLive == LastUserInput.Break && LapCounterLive == LastUserInput.Laps) || TimeTickSegment == SegmentNames.EndBreak)
                {
                    LapCounterLive--;
                    TimeTickSegment = SegmentNames.Work;
                    _countdownTimer.Start();
                    TimerTick = true;
                }
                // start from work segment
                else if ((WorkTimerLive > 0 || WorkTimerLive != LastUserInput.Work) && LapCounterLive >= 0)
                {
                    TimeTickSegment = SegmentNames.Work;
                    _countdownTimer.Start();
                    TimerTick = true;
                }
                // start from break
                else if (BreakTimerLive != LastUserInput.Break && WorkTimerLive <= 0 && LapCounterLive >= 0)
                {
                    //MessageBox.Show("break");
                    TimeTickSegment = SegmentNames.Break;
                    _countdownTimer.Start();
                    TimerTick = true;
                }
                else
                {
                    Set();
                    StartStop("Start");
                }
                return "Stop";
            }
            // Stop timer 
            else if (btnText == "Stop")
            {
                // Stop during break
                if (TimeTickSegment == SegmentNames.Break)
                {
                    throw new TimeoutException("Can not Stop during break time.");
                }
                // Stop after break ended 
                else if (TimeTickSegment == SegmentNames.EndBreak)
                {
                    if (LapCounterLive > 0)
                    {
                        StartLap();
                    }
                    else if (LapCounterLive <= 0)
                    {
                        EndLapPackage();
                    }
                    return "Start";
                }
                else
                {
                    if (TimeTickSegment != SegmentNames.Paused)
                    {
                        TimeTickSegment = SegmentNames.Paused;
                        _countdownTimer.Stop();
                        TimerTick = false; 
                    }

                    return "Start";
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("btnText", btnText, "Please report to the developer (CD 318)");
            }
        }

        /// <summary>
        /// Set last used values to the live countdowns.
        /// </summary>
        public static void Set()
        {
            WorkTimerLive = LastUserInput.Work;
            BreakTimerLive = LastUserInput.Break;
            LapCounterLive = LastUserInput.Laps;
        }

        /// <summary>
        /// Skipping to break form work segment.
        /// </summary>
        public static void SkipToBreak()
        {
            WorkTimerLive = 0;
            TimerController("Break");
        }

        /// <summary>
        /// Start to the next lap.
        /// </summary>
        public static void StartLap()
        {
            _countdownTimer.Stop();
            TimerTick = false;
            WorkTimerLive = LastUserInput.Work;
            BreakTimerLive = LastUserInput.Break;

            LapCounterLive--;
            TimeTickSegment = SegmentNames.Work;
            _countdownTimer.Start();
            TimerTick = true;
        }

        /// <summary>
        /// Finish up the last lap 
        /// </summary>
        public static void EndLapPackage()
        {
            Set();

            TimeTickSegment = SegmentNames.Paused;
            _countdownTimer.Stop();
            TimerTick = false;
        }

        /// <summary>
        /// pause after end of lap
        /// </summary>
        public static void PauseBetweenLap()
        {
            if (LapCounterLive <= 0)
            {
                EndLapPackage();
            }
            else
            {
                TimeTickSegment = SegmentNames.Paused;
                _countdownTimer.Stop();
                TimerTick = false;

                WorkTimerLive = LastUserInput.Work;
                BreakTimerLive = LastUserInput.Break;
            }
        }
        #endregion
    }
}