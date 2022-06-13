﻿using Marculator.Models;
using Microsoft.EntityFrameworkCore;

namespace Marculator
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
        public DbSet<Product> Products { get; set;  }
    }
}
