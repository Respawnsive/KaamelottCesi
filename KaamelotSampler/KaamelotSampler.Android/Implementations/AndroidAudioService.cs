using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KaamelotSampler.Droid.Implementations;
using KaamelotSampler.Interfaces;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidAudioService))]
namespace KaamelotSampler.Droid.Implementations
{
    public class AndroidAudioService : IAudioService
    {
        public void PlayMp3(string filename)
        {
            try
            {
                string filepath = Path.Combine("mp3", filename);
                var filedesc = Platform.CurrentActivity.Assets.OpenFd(filepath);
                MediaPlayer player = new MediaPlayer();
                player.SetDataSource(filedesc);
                player.Prepared += (s, e) =>
                {
                    player.Start();
                };
                player.Completion += (s, e) =>
                {
                    player.Dispose();
                };
                player.Prepare();
                
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}