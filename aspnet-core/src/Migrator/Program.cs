using API.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TasksDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));

            using (var dbContext = new TasksDbContext(optionsBuilder.Options))
            {
                Console.WriteLine("Running migrations...");
                dbContext.Database.Migrate();
            }

            Environment.Exit(0);
        }
    }
}