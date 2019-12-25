using DatingWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebApi.Data
{
    public class ApplicationDataContext: DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options): base(options)
        {

        }
        public DbSet<Value> Values { get; set; }
        public DbSet<User>  Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
