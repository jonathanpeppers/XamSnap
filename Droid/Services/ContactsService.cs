using System.Linq;
using System.Threading.Tasks;

namespace XamSnap.Droid
{
    public class ContactsService : IFriendService
    {
        public async Task<User[]> GetFriends(string userName)
        {
            var book = new Xamarin.Contacts.AddressBook(Application.Context);
            await book.RequestPermission();
            var contacts = book.ToArray();
            return contacts.Select(c => new User
            {
                Name = c.DisplayName,

            }).ToArray();
        }
    }
}

