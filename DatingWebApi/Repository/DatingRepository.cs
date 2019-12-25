using DatingWebApi.Data;
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

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Include(p => p.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll<T>() where T : class
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
