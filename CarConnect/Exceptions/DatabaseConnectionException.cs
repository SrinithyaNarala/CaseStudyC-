using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    internal class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException() : base("Database connection failed.")
        {
        }
        public DatabaseConnectionException(string message) : base(message)
        {
        }

        public DatabaseConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    
    public DatabaseConnectionException(string message, System.Data.SqlClient.SqlException sqlEx) : base(message) { }
    }
}
