using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Service.Services;
using System;
using System.IO;

namespace Service.Databases.Sql
{
    public class ServerDesignTimeDbContextFactory : DesignTimeDbContextFactory<ServerContext>
    {
    }

    public class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        public T CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<T>();
            var connectionString = configuration.GetSection(Define.Sql).Value;
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Service"));

            var dbContext = (T)Activator.CreateInstance(
                typeof(T),
                builder.Options);

            return dbContext;
        }
    }
}
