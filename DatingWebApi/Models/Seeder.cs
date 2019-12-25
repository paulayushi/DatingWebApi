using DatingWebApi.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatingWebApi.Models
{
    public static class Seeder
    {
        public static void DataSeeder(ApplicationDataContext context)
        {
            if (!context.Users.Any())
            {
                var seeds = File.ReadAllText("Data\\UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(seeds);
                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordSalt, out passwordHash);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();

                    context.Users.Add(user);
                }

                context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hashKey = new HMACSHA512())
            {
                passwordSalt = hashKey.Key;
                passwordHash = hashKey.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
