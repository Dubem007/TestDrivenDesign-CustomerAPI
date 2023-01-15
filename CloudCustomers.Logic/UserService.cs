using CloudCustomers.Interface;
using CloudCustomers.Logic.Models;
using CloudCustomers.Models.Config;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;

namespace CloudCustomers.Logic
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly UserApiOptions _config;

        public UserService(HttpClient httpClient, IOptions<UserApiOptions> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userResponse = await _httpClient.GetAsync(_config.Endpoint);
            if (userResponse.StatusCode == HttpStatusCode.NotFound)
                return new List<User>();

            var responseContent = userResponse.Content;
            var allusers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allusers.ToList();

        }
    }
}
