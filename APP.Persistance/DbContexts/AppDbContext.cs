
using APP.Domain.Entities.OTPDto;
using APP.Domain.Entities.UserDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Persistance.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

            public DbSet<User> Users { get; set; }
         
           public DbSet<OTPLog>OTPLogs { get; set; }
    }
}
