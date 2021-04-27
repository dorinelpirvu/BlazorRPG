using BlazorRPG.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Server.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<Character> Characters { get; set; }
        public DbSet<RpgClass> RpgClasses { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
