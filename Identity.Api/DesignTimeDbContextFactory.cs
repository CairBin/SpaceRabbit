﻿using CommonInit;
using IdentityService.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityService.Api
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdDbContext>
    {
        public IdDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = DbContextOptionsBuilderFactory.Create<IdDbContext>();
            return new IdDbContext(optionsBuilder.Options);
        }
    }
}
