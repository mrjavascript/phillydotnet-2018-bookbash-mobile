using System.Threading.Tasks;

namespace BookBash.Services
{
    public interface IAccountService
    {
        Task CreateUserAccount(string userName, string password, string emailAddress);
        Task AccountLogin(string userName, string password);
        bool IsLoggedIn();
        Task ResetPassword(string newPassword);
        void LogOutUser();
        Task FacebookAccountLogin(string email);
    }
}