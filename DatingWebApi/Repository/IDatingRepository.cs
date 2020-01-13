using DatingWebApi.Helper;
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
        public Task<bool> SaveAll();
        public Task<PagedList<User>> GetUsers(UserParams userParams);
        public Task<User> GetUser(int id);
        public Task<Photo> GetPhoto(int id);
        public Task<Photo> GetMainPhotoForUser(int userId);
    }
}
