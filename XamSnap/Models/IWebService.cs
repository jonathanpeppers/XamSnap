using System.Threading.Tasks;

namespace XamSnap
{
    public interface IWebService
    {
        Task<User> Register(User user);

        Task<User> AddFriend(string userName, string friendName);

        Task<Conversation[]> GetConversations(string userName);

        Task<Message[]> GetMessages(string conversation);

        Task<Message> SendMessage(Message message);
    }
}

