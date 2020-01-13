using DatingWebApi.Data;
using DatingWebApi.Helper;
using DatingWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebApi.Repository
{
    public class DatingRepository : IDatingRepository
    {
        private readonly ApplicationDataContext _context;

        public DatingRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            if(entity != null)
            {
                _context.Add<T>(entity);
            }
        }

        public void Delete<T>(T entity) where T : class
        {
            if(entity != null)
            {
                _context.Remove<T>(entity);
            }
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(p => p.UserId == userId).FirstOrDefaultAsync(p => p.IsMain == true);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.Include(u => u.Photos).AsQueryable();
            users = users.Where(u => u.Id != userParams.UserId && u.Gender == userParams.Gender);
            users = users.OrderByDescending(o => o.LastActive);
            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy.ToLower())
                {
                    case "created":
                        users = users.OrderByDescending(o => o.Created);
                        break;
                    default:
                        users = users.OrderByDescending(o => o.LastActive);
                        break;
                }                     
            }

            return await PagedList<User>.CreateAsync(users, userParams.CurrentPage, userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
