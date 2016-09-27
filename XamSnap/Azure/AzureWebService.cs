using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamSnap.Azure
{
    public class AzureWebService : IWebService
    {
        private IMobileServiceClient _client = new MobileServiceClient("https://xamsnap.azurewebsites.net");

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
