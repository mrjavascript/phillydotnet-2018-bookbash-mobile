using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using BookBash.Models.Request;
using BookBash.Models.Response;
using BookBash.Utility;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookBash.Services.Impl
{
    public class AccountService : IAccountService
    {
        private HttpClient _client;
        private readonly ICredentialsService _credentialsService;

        public AccountService()
        {
            // don't need to do this thanks to prism!
             _credentialsService = DependencyService.Get<ICredentialsService>();

            // set up the http client
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

        }

        public async Task CreateUserAccount(string userName, string password, string emailAddress)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            try
            {
                var userLoginRequest =
                    new UserCreateRequest {userName = userName, password = password, passwordConfirm = password, emailAddress = emailAddress};
                var json = JsonConvert.SerializeObject(userLoginRequest);
                var content = new StringContent(json);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = await _client.PostAsync(EnvironmentSettings.ApiUrl + "/user/create", content);
                var returnContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var returnUser = JsonConvert.DeserializeObject<ApplicationUserResponse>(returnContent);
                    _credentialsService.SaveCredentials(userLoginRequest.userName, userLoginRequest.password, returnUser.jwt);
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ApiError>(returnContent);
                    throw new Exception(error.message);
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                throw new Exception($"HTTP errors: {e.Message}");
            }
        }

        public async Task AccountLogin(string userName, string password)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            try
            {
                var userLoginRequest = new UserLoginRequest {userName = userName, password = password};
                var json = JsonConvert.SerializeObject(userLoginRequest);
                var content = new StringContent(json);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = await _client.PostAsync(EnvironmentSettings.ApiUrl + "/user/login", content);
                var returnContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var returnUser = JsonConvert.DeserializeObject<ApplicationUserResponse>(returnContent);
                    _credentialsService.SaveCredentials(userLoginRequest.userName, userLoginRequest.password, returnUser.jwt);
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ApiError>(returnContent);
                    throw new Exception(error.message);
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                throw new Exception($"HTTP errors: {e.Message}");
            }
        }

        public bool IsLoggedIn()
        {
            return _credentialsService.DoCredentialsExist();
        }

        public async Task ResetPassword(string newPassword)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            // need JWT
            var jwt = "Bearer " + _credentialsService.Jwt;
            _client.DefaultRequestHeaders.Add("Authorization", jwt);

            try
            {
                var values = new Dictionary<string, string>
                {
                    { "newPassword", newPassword }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await _client.PostAsync(EnvironmentSettings.ApiUrl + "/api/user/password/reset", content);
                var returnContent = await response.Content.ReadAsStringAsync();
                if (! response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<ApiError>(returnContent);
                    throw new Exception(error.message);
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                throw new Exception($"HTTP errors: {e.Message}");
            }
        }

        public void LogOutUser()
        {
            _credentialsService.DeleteCredentials();

            // other code you want to do
        }

        public async Task FacebookAccountLogin(string emailAddress)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            // need JWT
            var jwt = "Bearer " + _credentialsService.Jwt;
            _client.DefaultRequestHeaders.Add("Authorization", jwt);

            try
            {
                var values = new Dictionary<string, string>
                {
                    { "emailAddress", emailAddress }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await _client.PostAsync(EnvironmentSettings.ApiUrl + "/user/facebook-oauth-callback", content);
                var returnContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<ApiError>(returnContent);
                    throw new Exception(error.message);
                }

                var returnUser = JsonConvert.DeserializeObject<ApplicationUserResponse>(returnContent);
                _credentialsService.SaveCredentials(returnUser.userName, "abcde12345", returnUser.jwt);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                throw new Exception($"HTTP errors: {e.Message}");
            }
        }
    }
}