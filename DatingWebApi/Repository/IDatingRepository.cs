using DatingWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebApi.Repository
{
    public interface IDatingRepository
    {
        public void Add<T>(T entity) where T : class;
        public void Delete<T>(T entity) where T : class;
        public Task<bool> SaveAll<T>() where T : class;
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUser(int id);
    }
}
