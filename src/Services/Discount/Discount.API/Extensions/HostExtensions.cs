using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost host, int? retry = 0)
        {
            int? retryForAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuaration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<T>>();

                try
                {
                    logger.LogInformation("Migrating postgresql database.");

                    using var connection = new NpgsqlConnection(configuaration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(
                                            Id SERIAL PRIMARY KEY,
                                            ProductName VARCHAR(24) NOT NULL,
                                            Description TEXT,
                                            Amount INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO Coupon(ProductName, Description, Amount) VALUES
                                            ('IPhone X', 'IPhone X Discount', 150),
                                            ('Pixel 8', 'Pixel 8 Discount', 250),
                                            ('IPhone 15 Promax', 'IPhone 15 Promax Discount', 450),
                                            ('Pixel 8 Pro', 'Pixel 8 Pro Discount', 650);";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated postgresql database.");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database");
                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<T>(host, retryForAvailability);
                    }
                }
            }
            return host;
        }
    }   
}
