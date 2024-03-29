﻿using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace CommonInit
{
    public static class DbContextOptionsBuilderFactory
    {
        public static DbContextOptionsBuilder<TDbContext> Create<TDbContext>()
            where TDbContext : DbContext
        {
            var connStr = Environment.GetEnvironmentVariable("DefaultDB:ConnStr");
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
            return optionsBuilder;
        }
    }
}
