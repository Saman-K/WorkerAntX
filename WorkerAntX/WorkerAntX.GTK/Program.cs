using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace WorkerAntX.GTK
{
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Gtk.Application.Init();
            Forms.Init();

            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("Worker Ant X");
            window.Show();

            Gtk.Application.Run();
        }
    }
}