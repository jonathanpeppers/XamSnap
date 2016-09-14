using System.Linq;
using System.Threading.Tasks;

namespace XamSnap.iOS
{
    public class ContactsService : IFriendService
    {
        public async Task<User[]> GetFriends(string userName)
        {
            var book = new Xamarin.Contacts.AddressBook();
            await book.RequestPermission();
            return book.Select(c => new User
            {
                Name = c.DisplayName,

            }).ToArray();
        }
    }
}

