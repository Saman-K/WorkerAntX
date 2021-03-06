﻿using Android.App;
using Android.Content;
using Android.Support.V4.App;
using System;
using WorkerAntX.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationHelper))]
namespace WorkerAntX.Droid
{
    class NotificationHelper : INotification
    {
        private Context mContext;
        //private NotificationManager mNotificationManager;
        //private NotificationCompat.Builder mBuilder;
        public static String NOTIFICATION_CHANNAL_ID = "1";
        private string StartStop
        {
            get
            {

                if (Countdown.TimerTick == true)
                {
                    return "Stop";
                }
                else
                {
                    return "Start";
                }
            }
        }

        public NotificationHelper()
        {
            mContext = global::Android.App.Application.Context;
        }

        [Obsolete]
        public void CreateNotification(string sub, string titel, string message, int progress)
        {

            try
            {
                var mBuilder = new NotificationCompat.Builder(mContext);

                ////Create intent for action 1(TAKE)
                //var actionIntent1 = new Intent();
                //actionIntent1.SetAction(action_sjklf)
                //var pIntent1 = PendingIntent.GetBroadcast(mContext, 0, actionIntent1, PendingIntentFlags.CancelCurrent);
                //// var intsas = PendingIntent.GetActivity(mContext, 0, actionIntent1, 0);


                if (Countdown.TimerTick == true)
                {
                    mBuilder//.AddAction(Resource.Mipmap.icon, "Stop", pIntent1)
                            .SetContentText(message)
                            .SetProgress(1000, progress, false);
                }
                else if (Countdown.TimerTick == false)
                {
                    //mBuilder.AddAction(Resource.Mipmap.icon, "Start", pIntent1);
                }
                mBuilder.SetAutoCancel(true)
                    .SetContentTitle(titel)
                    .SetSubText(sub)
                    .SetChannelId(NOTIFICATION_CHANNAL_ID)
                    .SetPriority((int)NotificationPriority.Low)
                    .SetVisibility((int)NotificationVisibility.Public)
                    .SetSmallIcon(Resource.Mipmap.icon);




                //// Add intent filters for each action and register them on a broadcast receiver
                //var intentFilter = new IntentFilter();
                //intentFilter.AddAction("Start");
                //intentFilter.AddAction("Stop");

                //var customReceiver = new CustomActionReceiver();

                //mContext.RegisterReceiver(customReceiver, intentFilter);
                ////

                NotificationManager notificationManager = mContext.GetSystemService(Context.NotificationService) as NotificationManager;

                if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
                {
                    NotificationImportance importance = global::Android.App.NotificationImportance.Default;

                    NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNAL_ID, titel, importance);
                    notificationChannel.SetShowBadge(true);


                    if (notificationManager != null)
                    {
                        mBuilder.SetChannelId(NOTIFICATION_CHANNAL_ID);
                        notificationManager.CreateNotificationChannel(notificationChannel);
                    }
                }
                notificationManager.Notify(0, mBuilder.Build());

            }
            catch (Exception ex)
            {
                //
            }


        }
    }
}