using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerAntX
{
    public interface INotification
    {
        void CreateNotification(string sub, string titel, string message, int progress);
    }
}
