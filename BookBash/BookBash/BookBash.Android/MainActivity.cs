using Android.App;
using Android.Content.PM;
using Android.OS;

namespace BookBash.Droid
{
    //    [Activity(Label = "BookBash", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [Activity(Label = "BookBash", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));

            // Locks Screen in Portrait Mode
            RequestedOrientation = ScreenOrientation.Portrait;

            //
            //  If clicks from the widget...
            if (Intent.GetStringExtra(WidgetAction.Extra) == null) return;
            if (Intent.GetStringExtra(WidgetAction.Extra) != WidgetAction.Dashboard) return;
            await ((App)Xamarin.Forms.Application.Current).HandleWidgetAction();
        }
    }
}

