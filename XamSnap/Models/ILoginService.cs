using System.Threading.Tasks;

namespace XamSnap
{
    public interface ILoginService
    {
        Task<User> Login(string userName, string password);
    }
}
