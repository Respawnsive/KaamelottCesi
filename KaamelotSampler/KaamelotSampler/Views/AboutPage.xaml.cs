using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.GoogleMaps;

namespace KaamelotSampler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            mymap.MoveToRegion(
                new MapSpan(
                    new Position(50.62901236535048, 3.071320046022258), 0.2, 0.2));
        }
    }
}