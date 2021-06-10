using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.interfaces;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;
        public UserRepository(DataContext dc)
        {
            this.dc = dc;

        }
        public async Task<User> Authenticate(string userName, string password)
        {
            return await this.dc.Users.FirstOrDefaultAsync(x => x.Username == userName && x.Password == password);
        }
    }
}