using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkerAntX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        #region Methods
        //------------------------------------------------------------------------- Button
        // Default Button
        private void DefaultBtn(object sender, EventArgs e)
        {

        }
        // Save Button
        private void SaveBtn(object sender, EventArgs e)
        {

        }
        #endregion
    }
}