using KaamelotSampler.Models;
using KaamelotSampler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KaamelotSampler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailSamplePage : ContentPage
    {
        public DetailSamplePage(Saample currentSample)
        {
            InitializeComponent();
            BindingContext = new DetailSamplePageViewModel(Navigation, currentSample);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Task.WhenAll(
                ImgPerso.RotateTo(360, 2000),
                ImgPerso.ScaleTo(0.5, 2000),
                ImgPerso.FadeTo(0.5, 2000)
                );
            await Task.WhenAll(
                ImgPerso.RotateTo(0, 2000),
                ImgPerso.ScaleTo(1, 2000),
                ImgPerso.FadeTo(1, 2000)
                );
        }
    }
}