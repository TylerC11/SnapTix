using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SnapTix.Models;

namespace SnapTix.Data
{
    public class SnapTixContext : DbContext
    {
        public SnapTixContext (DbContextOptions<SnapTixContext> options)
            : base(options)
        {
        }

        public DbSet<SnapTix.Models.Sport> Sport { get; set; } = default!;
        public DbSet<SnapTix.Models.Owner> Owner { get; set; } = default!;
        public DbSet<SnapTix.Models.Category> Category { get; set; } = default!;
    }
}
