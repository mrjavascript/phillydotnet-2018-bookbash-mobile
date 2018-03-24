using System.Diagnostics;
using System.Linq;
using BookBash.iOS;
using BookBash.Services;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(CredentialsService))]
namespace BookBash.iOS
{
    public class CredentialsService : ICredentialsService
    {
        public string UserName
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return account?.Username;
            }
        }

        public string Password
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return account?.Properties["Password"];
            }
        }

        public string Jwt
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return account?.Properties["jwt"];
            }
        }

        public void SaveCredentials(string userName, string password, string jwt)
        {
            Debug.WriteLine("Called Save Credential");
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(jwt)) return;
            Account account = new Account
            {
                Username = userName
            };
            account.Properties.Add("Password", password);
            account.Properties.Add("jwt", jwt);
            AccountStore.Create().Save(account, App.AppName);

            Debug.WriteLine("new account saved to account store");

        }

        public void DeleteCredentials()
        {

            Debug.WriteLine("Delete Service Called");
            var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
            if (account != null)
            {
                AccountStore.Create().Delete(account, App.AppName);
            }

            account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
            if (account != null)
            {
                Debug.WriteLine("Delete Credential Fails");
            }
        }

        public bool DoCredentialsExist()
        {
            return AccountStore.Create().FindAccountsForService(App.AppName).Any();
        }
    }
}