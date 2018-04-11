using Npgsql;
using System.Configuration;

namespace ITI.GateIn.Console.DAL
{
    public class AppConfig
    {
        public const string ConnectionString = "Server={0};Port={1};UserId={2};Password={3};Database={4};";

        public static NpgsqlConnection GetLoginConnection()
        {
            string loginServer = "localhost";
            string port = "5432";
            string userId = "agys";
            string password = "agys123";
            string database = "ictslogin";
            if (ConfigurationManager.AppSettings["LoginServer"] != null)
            {
                loginServer = ConfigurationManager.AppSettings["LoginServer"];
            }
            if (ConfigurationManager.AppSettings["Port"] != null)
            {
                port = ConfigurationManager.AppSettings["Port"];
            }
            if (ConfigurationManager.AppSettings["UserId"] != null)
            {
                port = ConfigurationManager.AppSettings["UserId"];
            }
            if (ConfigurationManager.AppSettings["LoginDatabase"] != null)
            {
                port = ConfigurationManager.AppSettings["LoginDatabase"];
            }
            string connectionString = string.Format(ConnectionString,
                loginServer,
                port,
                userId,
                password,
                database
            );
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString);
            return npgsqlConnection;
        }

        public static NpgsqlConnection GetConnection()
        {
            string loginServer = "192.168.15.1";
            //string port = "5032";
            string port = "5432";
            string userId = "agys";
            string password = "agys123";
            string database = "icts";
            if (ConfigurationManager.AppSettings["LoginServer"] != null)
            {
                loginServer = ConfigurationManager.AppSettings["LoginServer"];
            }
            if (ConfigurationManager.AppSettings["Port"] != null)
            {
                port = ConfigurationManager.AppSettings["Port"];
            }
            if (ConfigurationManager.AppSettings["UserId"] != null)
            {
                userId = ConfigurationManager.AppSettings["UserId"];
            }
            if (ConfigurationManager.AppSettings["Database"] != null)
            {
                database = ConfigurationManager.AppSettings["Database"];
            }
            string connectionString = string.Format(ConnectionString,
                loginServer,
                port,
                userId,
                password,
                database
            );
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString);
            return npgsqlConnection;
        }

        public static NpgsqlConnection GetUserConnection()
        {
            string connectionString = string.Format(ConnectionString + "Pooling = True; MaxPoolSize = 60;","192.168.15.1","5032","rizki","rizki123","icts");
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString);
            return npgsqlConnection;
        }
    }
}
