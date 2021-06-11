using WebAPI.Models;

using System.Threading.Tasks;

namespace WebAPI.interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
        void Register(string userNamem, string password);
        Task<bool> UserAlreadyExist(string username);
    }
}