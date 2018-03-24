using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using BookBash.Models;
using BookBash.Models.Response;
using BookBash.Utility;
//using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookBash.Services.Impl
{
    public class BacklogService : IBacklogService
    {
        private HttpClient _client;
        private readonly ICredentialsService _credentialsService;

        public BacklogService()
        {
            _credentialsService = DependencyService.Get<ICredentialsService>();

            // set up the http client
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task DeleteBacklogItem(long itemRecordId)
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
                    { "recordId", itemRecordId.ToString() }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await _client.PostAsync(EnvironmentSettings.ApiUrl + "/api/backlog/delete", content);
                var returnContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
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

        public async Task<List<BacklogStatus>> GetBacklogStatuses()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            // need JWT
            var jwt = "Bearer " + _credentialsService.Jwt;
            _client.DefaultRequestHeaders.Add("Authorization", jwt);

            try
            {
                var response = await _client.GetAsync(EnvironmentSettings.ApiUrl + "/api/backlog/statuses");
                var returnContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<BacklogStatus>>(returnContent);
                var error = JsonConvert.DeserializeObject<ApiError>(returnContent);
                throw new Exception(error.message);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                throw new Exception($"HTTP errors: {e.Message}");
            }
        }

        public async Task AddBacklogItem(long bookId, long statusId, double rating)
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
                    { "bookId" , bookId.ToString() },
                    { "statusId", statusId.ToString()},
                    { "rating" , rating.ToString(CultureInfo.InvariantCulture) }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await _client.PostAsync(EnvironmentSettings.ApiUrl + "/api/backlog/add", content);
                var returnContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
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

        public async Task EditBacklogItem(long recordId, long statusId, double rating, long bookId)
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
                    { "recordID" , recordId.ToString() },
                    { "bookId" , bookId.ToString() },
                    { "statusId", statusId.ToString()},
                    { "rating" , rating.ToString(CultureInfo.InvariantCulture) }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await _client.PostAsync(EnvironmentSettings.ApiUrl + "/api/backlog/edit", content);
                var returnContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
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

        public async Task<Book> FindBookByIsbn(string isbn)
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
                    { "isbn", isbn }
                };
                // var url = String.Empty;  //QueryHelpers.AddQueryString(EnvironmentSettings.ApiUrl + "/api/backlog/search", values);

                var builder = new UriBuilderExt(EnvironmentSettings.ApiUrl + "/api/backlog/search");
                builder.AddParameter("isbn", isbn);
                var response = await _client.GetAsync(builder.Uri);
                var returnContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<Book>(returnContent);
                var error = JsonConvert.DeserializeObject<ApiError>(returnContent);
                throw new Exception(error.message);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                throw new Exception($"HTTP errors: {e.Message}");
            }
        }

        public async Task<List<BacklogItem>> GetUserBacklog()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            // need JWT
            var jwt = "Bearer " + _credentialsService.Jwt;
            _client.DefaultRequestHeaders.Add("Authorization", jwt);

            try
            {
                var response = await _client.GetAsync(EnvironmentSettings.ApiUrl + "/api/backlog/list");
                var returnContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<BacklogItem>>(returnContent);
                var error = JsonConvert.DeserializeObject<ApiError>(returnContent);
                throw new Exception(error.message);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                throw new Exception($"HTTP errors: {e.Message}");
            }
        }
    }
}