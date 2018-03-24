using System.Linq;
using BookBash.Droid;
using BookBash.Services;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(CredentialsService))]
namespace BookBash.Droid
{
    public class CredentialsService : ICredentialsService
    {
        public string UserName
        {
            get
            {
                var account = AccountStore.Create(Android.App.Application.Context).FindAccountsForService(App.AppName).FirstOrDefault();
                return account?.Username;
            }
        }

        public string Password
        {
            get
            {
                var account = AccountStore.Create(Android.App.Application.Context).FindAccountsForService(App.AppName).FirstOrDefault();
                return account?.Properties["Password"];
            }
        }

        public string Jwt
        {
            get
            {
                var account = AccountStore.Create(Android.App.Application.Context).FindAccountsForService(App.AppName).FirstOrDefault();
                return account?.Properties["jwt"];
            }
        }

        public void SaveCredentials(string userName, string password, string jwt)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)) return;
            var account = new Account
            {
                Username = userName
            };
            account.Properties.Add("Password", password);
            account.Properties.Add("jwt", jwt);
            AccountStore.Create(Android.App.Application.Context).Save(account, App.AppName);
        }

        public void DeleteCredentials()
        {
            var account = AccountStore.Create(Android.App.Application.Context).FindAccountsForService(App.AppName).FirstOrDefault();
            if (account != null)
            {
                AccountStore.Create(Android.App.Application.Context).Delete(account, App.AppName);
            }
        }


        public bool DoCredentialsExist()
        {
            return AccountStore.Create(Android.App.Application.Context).FindAccountsForService(App.AppName).Any();
        }
    }
}