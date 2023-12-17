using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace DatabaseInitializer
{
    class Program
    {
        private static readonly string Password = Environment.GetEnvironmentVariable("hdrPassword") 
            ?? throw new InvalidOperationException("You must set the hdrPassword environment variable");
        private static readonly string Database = Environment.GetEnvironmentVariable("hdrDatabase") 
            ?? throw new InvalidOperationException("You must set the hdrDatabase environment variable");
        private static readonly string Port = Environment.GetEnvironmentVariable("hdrPort") 
            ?? throw new InvalidOperationException("You must set the hdrPort environment variable");
        private static readonly string SchemaLocation = Environment.GetEnvironmentVariable("HdrSchemaLocation") 
            ?? throw new InvalidOperationException("You must set the HdrSchemaLocation environment variable");

        static async Task Main(string[] args)
        {
            Console.WriteLine("Waiting for database to start");
            await TestConnection();

            Console.WriteLine("Adding new database");            
            await CreateDatabase();
            
            Console.WriteLine("Adding database schema");
            await ImportSchema();
        }

        private static async Task TestConnection()
        {
            Exception? latestException = null;
            var then = DateTime.UtcNow;
            while (DateTime.UtcNow - then < TimeSpan.FromMinutes(0.2))
            {
                try
                {
                    using var cnxn = new NpgsqlConnection($"Server=localhost; User ID=postgres; Password={Password}; Port={Port};");
                    await cnxn.OpenAsync();
                    Console.WriteLine("Connection attempt succeeded");

                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Connection attempt failed");
                    latestException = e;
                    await Task.Delay(1000);
                }
            }

            throw new InvalidOperationException($"Could not connect to database", latestException);
        }

        private static async Task CreateDatabase()
        {
            using var cnxn = new NpgsqlConnection($"Server=localhost; User ID=postgres; Password={Password}; Port={Port};");
            await cnxn.OpenAsync();

            var command = cnxn.CreateCommand();
            command.CommandText = $"CREATE DATABASE {Database}";

            await command.ExecuteNonQueryAsync();
        }

        private static async Task ImportSchema()
        {
            using var cnxn = new NpgsqlConnection($"Server=localhost; User ID=postgres; Password={Password}; Port={Port}; Database={Database};");
            await cnxn.OpenAsync();

            var command = cnxn.CreateCommand();
            command.CommandText = await File.ReadAllTextAsync(SchemaLocation);

            await command.ExecuteNonQueryAsync();
        }
    }
}
