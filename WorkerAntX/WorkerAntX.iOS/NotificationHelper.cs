using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkerAntX.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationHelper))]
namespace WorkerAntX.iOS
{
    class NotificationHelper : INotification
    {
        public void CreateNotification(string title, string message)
        {
            new NotificationDelegate().RegisterNotification(title, message);
        }
    }
}