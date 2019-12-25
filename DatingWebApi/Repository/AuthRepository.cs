using DatingWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using DatingWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DatingWebApi.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDataContext _context;

        public AuthRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        public async Task<bool> IsUserExist(string username)
        {
            if (await _context.Users.AnyAsync(x=> x.Username == username))
                return true;
            return false;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
            if (user == null)
                return null;
            if (!VerifyPassword(user.PasswordSalt, user.PasswordHash, password))
                return null;
            
            return user;
        }

        private bool VerifyPassword(byte[] passwordSalt, byte[] passwordHash, string password)
        {
            using(var hashKey = new HMACSHA512(passwordSalt))
            {
                var computedPassword = hashKey.ComputeHash(Encoding.UTF8.GetBytes(password));
                for(int i=0; i< computedPassword.Length; i++)
                {
                    if (computedPassword[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordSalt, out passwordHash);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            await _context.AddAsync<User>(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hashKey = new HMACSHA512())
            {
                passwordSalt = hashKey.Key;
                passwordHash = hashKey.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
