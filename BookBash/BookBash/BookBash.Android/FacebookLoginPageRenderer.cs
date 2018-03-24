using System;
using Android.App;
using Android.Content;
using BookBash.Droid;
using BookBash.Views;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Xamarin.Forms.Application;

[assembly:ExportRenderer(typeof(FacebookPage), typeof(FacebookLoginPageRenderer))]
namespace BookBash.Droid
{
    public class FacebookLoginPageRenderer : PageRenderer
    {
        public FacebookLoginPageRenderer(Context context) : base(context)
        {
            var activity = Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: "155359068487440",
                scope: "email",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth"),
                redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                    var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=id,name,picture,email"), null,
                        eventArgs.Account);
                    var response = await request.GetResponseAsync();
                    var obj = JObject.Parse(response.GetResponseText());

                    var id = obj["id"].ToString().Replace("\"", string.Empty);
                    var name = obj["name"].ToString().Replace("\"", string.Empty);
                    var email = obj["email"].ToString().Replace("\"", string.Empty);
                    
                    ((App)Application.Current).SuccessfulFacebookLogin(email);
                }
                else
                {
                    ((App)Application.Current).CancelFacebookLogin();
                }
            };

            activity?.StartActivity(auth.GetUI(activity));
        }
        
    }
}