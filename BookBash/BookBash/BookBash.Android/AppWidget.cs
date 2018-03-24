using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;

namespace BookBash.Droid
{
    /*
     *  TUTORIAL: https://marcofolio.net/app-widget-xamarin-android/
     */

    [BroadcastReceiver(Label = "HellApp Widget")]
    [IntentFilter(new[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/appwidgetprovider")]
    public class AppWidget : AppWidgetProvider
    {
        /// <inheritdoc />
        /// <summary>
        /// This method is called when the 'updatePeriodMillis' from the AppwidgetProvider passes,
        /// or the user manually refreshes/resizes.
        /// </summary>
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            foreach (var appWidgetId in appWidgetIds)
            {
                // Get the layout for the App Widget and attach an on-click listener
                // to the button
                var views = new RemoteViews(context.PackageName, Resource.Layout.Widget);

                // Add dashboard button
                var addIntent = new Intent(context, typeof(MainActivity));
                addIntent.PutExtra(WidgetAction.Extra, WidgetAction.Dashboard);
                var pendingAddIntent = PendingIntent.GetActivity(context, 0, addIntent, PendingIntentFlags.UpdateCurrent);
                views.SetOnClickPendingIntent(Resource.Id.widget_dashboard, pendingAddIntent);

                // Tell the AppWidgetManager to perform an update on the current app
                // widget
                appWidgetManager.UpdateAppWidget(appWidgetId, views);
            }
        }
    }
}