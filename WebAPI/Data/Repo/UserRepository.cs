using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.interfaces;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System;

namespace WebAPI.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;
        public UserRepository(DataContext dc)
        {
            this.dc = dc;

        }
        public async Task<User> Authenticate(string userName, string passwordText)
        {
            var user = await dc.Users.FirstOrDefaultAsync(x => x.Username == userName);
            if(user == null || user.PasswordKey == null)
                return null;
            if(!MatchingPasswordHash(passwordText, user.Password, user.PasswordKey))
                return null;
            return user;
        }

        private bool MatchingPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using(var hsma = new HMACSHA512(passwordKey))
            {
                var passwordHash = hsma.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));
                for(int i=0;i<passwordHash.Length;i++)
                {
                    if(passwordHash[i] != password[i])
                        return false;
                }
                return true;
            }
        }

        public void Register(string userName, string password)
        {
            byte[] passwordKey, passwordHash;

            using(var hsma = new HMACSHA512())
            {
                passwordKey = hsma.Key;
                passwordHash = hsma.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            User user = new User();
            user.Username = userName;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;
            dc.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExist(string username)
        {
            return await dc.Users.AnyAsync(x => x.Username == username);
        }
    }
}