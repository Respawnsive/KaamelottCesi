using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace KaamelotSampler.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                var app = ConfigureApp.Android
                    .InstalledApp("com.companyname.kaamelotsampler")
                    .ConnectToApp();
                return app;
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}