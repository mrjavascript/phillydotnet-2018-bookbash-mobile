using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace BookBash.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .DeviceSerial("YOUR_ANDROID_DEVICE_ID_HERE")
//                    .ApkFile("C:\\Users\\mikem\\source\\repos\\book-bash-mobile-2\\BookBash\\BookBash\\BookBash.Android\\bin\\Release\\com.companyname.BookBash.apk")
                    .InstalledApp("com.companyname.BookBash")
                    .PreferIdeSettings()
//                    .InstalledApp("com.companyname.BookBash")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}

