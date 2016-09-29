using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XamSnap
{
    public class AzureFunctionsWebService : IWebService
    {
        private const string BaseUrl = "https://xamsnap.azurewebsites.net/api/";
        private const string Code = "n49qfil4y79il6yezfuwyiudi6avxyn09coyk4urlfh83b7f1orrgye9uaaupq6w82vp36czyqfr";
        private const string ContentType = "application/json";
        private readonly HttpClient _httpClient = new HttpClient();

        public Task<User> AddFriend(string userName, string friendName)
        {
            throw new NotImplementedException();
        }

        public Task<Conversation[]> GetConversations(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<Message[]> GetMessages(string conversation)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Login(string userName, string password)
        {
            string json = JsonConvert.SerializeObject(new
            {
                userName,
                password,
            });
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

            var response = await _httpClient.PostAsync(BaseUrl + "login?code=" + Code, content);
            response.EnsureSuccessStatusCode();

            return new User
            {
                Name = userName,
                Password = password,
            };
        }

        public Task<User> Register(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
