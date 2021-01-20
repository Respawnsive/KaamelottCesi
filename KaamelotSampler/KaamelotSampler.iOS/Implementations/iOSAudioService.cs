using AVFoundation;
using Foundation;
using KaamelotSampler.Interfaces;
using KaamelotSampler.iOS.Implementations;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(iOSAudioService))]
namespace KaamelotSampler.iOS.Implementations
{
    public class iOSAudioService : IAudioService
    {
        public void PlayMp3(string filename)
        {
            try
            {
                NSUrl filepath = new NSUrl("Sounds/" + filename);
                NSError error;
                AVAudioPlayer player = new AVAudioPlayer(filepath, "mp3", out error);
                player.Volume = 1;
                player.FinishedPlaying += delegate
                {
                    player = null;
                };
                player.NumberOfLoops = 0;
                player.Play();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}