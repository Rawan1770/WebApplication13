using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication13.Models;

namespace WebApplication13.Data
{
    public class WebApplication13Context : DbContext
    {
        public WebApplication13Context (DbContextOptions<WebApplication13Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication13.Models.userall> userall { get; set; } = default!;

        public DbSet<WebApplication13.Models.items>? items { get; set; }
    }
}
