using KaamelotSampler.Interfaces;
using KaamelotSampler.Models;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KaamelotSampler.ViewModels
{
    public class DetailSamplePageViewModel : BaseViewModel
    {
        private INavigation NavigationService;

        public DetailSamplePageViewModel(INavigation navigationService, Saample currentSample)
        {
            NavigationService = navigationService;
            //Garder le sample en mémoire -> currentSample
            CurrentSample = currentSample;

            PlayMP3Command = new Command(PlayMP3CommandExecute);
            PlayTTSCommand = new Command(PlayTTSCommandExecute);
            ShareCommand = new Command(ShareCommandExecute);
            MyTitre = $"Personnage : { currentSample.Character}";
        }


        private Saample currentSample;
        public Saample CurrentSample
        {
            get { return currentSample; }
            set 
            { 
                currentSample = value;
                OnPropertyChange("CurrentSample");
            }
        }

        public ICommand PlayMP3Command { get; }
        private void PlayMP3CommandExecute()
        {
            Task.Factory.StartNew(async() =>
            {
                IsBusy = true;
                string mp3toplay = CurrentSample?.File;
                if (!String.IsNullOrWhiteSpace(mp3toplay))
                {
                    var implementation = DependencyService.Get<IAudioService>();
                    if (implementation != null)
                        implementation.PlayMp3(mp3toplay);
                }
                await Task.Delay(2000);
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("Mp3", mp3toplay);
                Analytics.TrackEvent("PlayedMP3", dict);
                IsBusy = false;
            });
        }

        public ICommand PlayTTSCommand { get; }
        private async void PlayTTSCommandExecute()
        {
            try
            {
                var locales = await TextToSpeech.GetLocalesAsync();
                // Grab the first locale
                var locale = locales.Where(x => x.Country == "FR").FirstOrDefault();
                if (locale == null)
                    locale = locales.FirstOrDefault();

                var settings = new SpeechOptions()
                {
                    Volume = 1.0f,
                    Pitch = 1.2f,
                    Locale = locale
                };
                await TextToSpeech.SpeakAsync(CurrentSample.Title, settings);
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("File", CurrentSample.File);
                Analytics.TrackEvent("TTSSpokenMP3", dict);
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        public ICommand ShareCommand { get; }
        private async void ShareCommandExecute()
        {
            //string filepath = "";
            //if (Device.RuntimePlatform == Device.Android)
            //{
            //    filepath = @"file:///android_asset/mp3/" + CurrentSample.File;
            //}
            //else if (Device.RuntimePlatform == Device.iOS)
            //{
            //    filepath = $"/sounds/{ CurrentSample.File }";
            //}

            //if (File.Exists(filepath))
            //{

            //ShareFileRequest request = new ShareFileRequest();
            //request.Title = "Partage d'un son Kaamelot de " + CurrentSample.Character;
            //request.File = new ShareFile(filepath);
            ShareTextRequest request = new ShareTextRequest();
            request.Title = "Partager avec...";
            request.Text = "Partage d'un son Kaamelot de " + CurrentSample.Character;
            request.Uri = @"http://www.google.fr";
            await Share.RequestAsync(request);
            //}
        }


    }
}
