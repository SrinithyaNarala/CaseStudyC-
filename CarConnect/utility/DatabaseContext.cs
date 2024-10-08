using CarConnect.Exceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.utility
{
    internal class DatabaseContext
    {
        
        private static IConfiguration _iconfiguration;
        private IConfiguration configuration;

        static DatabaseContext() {
            GetAppSettingsFile();
        }

        public DatabaseContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private static void GetAppSettingsFile()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)// sets the base path where configuration file is located
                             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);// configuration should include  the configuration file named as appsettings,json 
                _iconfiguration = builder.Build();//build creates an Iconfiguration object which has data from Appsettings file
                Console.WriteLine($"Connection String: {_iconfiguration["ConnectionStrings:LocalConnectionString"]}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new InvalidOperationException("Failed to load database configuration", ex);
            }
        }

        public static string GetConnString()
        {
            if (_iconfiguration == null)
            {
                throw new InvalidOperationException("The configuration is not loaded. Check if 'appsettings.json' exists and is formatted correctly.");
            }

            var connectionString = _iconfiguration.GetConnectionString("LocalConnectionString");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is empty. Ensure 'appsettings.json' contains a valid connection string.");
            }
            return _iconfiguration.GetConnectionString("LocalConnectionString");
        }

    }
}
