﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XamSnap
{
    public class AzureFunctionsWebService : IWebService, IFriendService
    {
        private const string BaseUrl = "https://xamsnap.azurewebsites.net/api/";
        private const string ContentType = "application/json";
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<User> AddFriend(string userName, string friendName)
        {
            string json = JsonConvert.SerializeObject(new
            {
                userName,
                friendName
            });
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

            var response = await _httpClient.PostAsync(BaseUrl + "addfriend?code=cyp0asakll1yfdus4vsk5u3dieves1y4rlldj0lphoj013v7viw9zxsb4vzxn3cy6rafmg9cnmi", content);
            response.EnsureSuccessStatusCode();

            return new User
            {
                Name = friendName,
            };
        }

        public async Task<User[]> GetFriends(string userName)
        {
            string json = JsonConvert.SerializeObject(new
            {
                userName
            });
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

            var response = await _httpClient.PostAsync(BaseUrl + "friends?code=w0to2o614csk8e3iduc8fri7erkowfmuoavgxb6g2o11yvin6achwnsgecxgg6wqyeigrpb9", content);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User[]>(json);
        }

        public async Task<Conversation[]> GetConversations(string userName)
        {
            string json = JsonConvert.SerializeObject(new
            {
                userName
            });
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

            var response = await _httpClient.PostAsync(BaseUrl + "conversations?code=hsfqtj34ejgmujbpnxyjy8pvi79lgvj19bai5u9htd477tu766re5fpy1vm77vtn2imeyrkbuik9", content);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Conversation[]>(json);
        }

        public async Task<Message[]> GetMessages(string conversation)
        {
            string json = JsonConvert.SerializeObject(new
            {
                conversation
            });
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

            var response = await _httpClient.PostAsync(BaseUrl + "messages?code=af6rdk8tdnx0hqi0gph7zxgvihmiu5k1l86j1ihrgtbs0v0a4ifai28nifb3q3zz3lwr3cba9k9", content);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message[]>(json);
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

            var response = await _httpClient.PostAsync(BaseUrl + "login?code=n49qfil4y79il6yezfuwyiudi6avxyn09coyk4urlfh83b7f1orrgye9uaaupq6w82vp36czyqfr", content);
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