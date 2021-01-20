using KaamelotSampler.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KaamelotSampler
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppCenter.Start("android=a15bb087-4162-49c3-b5c9-4a162ea2a986;ios=aec67ecd-cf51-4c21-b370-d2e8ce931aa7", typeof(Analytics), typeof(Crashes));

            MainPage = new MDPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            //sauvegarder
        }

        protected override void OnResume()
        {
            //réafficher
        }
    }
}
