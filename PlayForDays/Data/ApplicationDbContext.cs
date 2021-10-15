using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlayForDays.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayForDays.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Sport> Sports { get; set; }
        public DbSet<SportingEvent> SportingEvents { get; set; }
        public DbSet<Player> Players { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
