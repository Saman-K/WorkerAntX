using Android.Content;
using Android.Widget;

namespace WorkerAntX.Droid
{
    public class CustomActionReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {

            //if (intent == )
            //{

            //}
            // Show toast here
            Toast.MakeText(context, intent.Action, ToastLength.Short).Show();

            var extras = intent.Extras;
            if (extras != null && !extras.IsEmpty)
            {
                //NotificationManager manager = context.GetSystemService(Context.NotificationService) as NotificationManager;
                //var notificationId = extras.GetInt("NotificationIdKey", -1);
                //if (notificationId != -1)
                //{
                //    manager.Cancel(notificationId);
                //}
            }
        }
    }

}