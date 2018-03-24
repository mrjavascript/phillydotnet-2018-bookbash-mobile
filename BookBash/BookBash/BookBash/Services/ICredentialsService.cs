namespace BookBash.Services
{
    public interface ICredentialsService
    {
        string UserName { get; }

        string Password { get; }

        string Jwt { get; }

        void SaveCredentials(string userName, string password, string jwt);

        void DeleteCredentials();

        bool DoCredentialsExist();
    }
}