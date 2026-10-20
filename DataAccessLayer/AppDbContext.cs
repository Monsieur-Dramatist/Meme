using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemeApp.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    internal class AppDbContext:DbContext
    {
        public DbSet<Meme> Memes { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext>  options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
