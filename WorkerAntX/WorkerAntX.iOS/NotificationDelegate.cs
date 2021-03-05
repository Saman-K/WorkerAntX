using Foundation;
using System;
using UserNotifications;

namespace WorkerAntX.iOS
{
    internal class NotificationDelegate : UNUserNotificationCenterDelegate
    {
        public NotificationDelegate() {}
        //public override void WillPresentNotification(UNUserNotificationCenter center, UNUserNotification notification, Action<UNUserNotificationCenterDelegate);

        public void RegisterNotification(string title, string body)
        {
            UNUserNotificationCenter center = UNUserNotificationCenter.Current;

            UNMutableNotificationContent notificationContent = new UNMutableNotificationContent();

            notificationContent.Title = title;
            notificationContent.Body = body;

            notificationContent.Sound = UNNotificationSound.Default;

            UNTimeIntervalNotificationTrigger trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(1, false);
            UNNotificationRequest request = UNNotificationRequest.FromIdentifier("FiveSecond", notificationContent, trigger);

            center.AddNotificationRequest(request, (NSError obj) =>
            {

            });
        }

        internal void RegisterNotification(object title, string message)
        {
            throw new NotImplementedException();
        }
    }
}