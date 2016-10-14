using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public class Access
    {
        #region Fields
        /// <summary>
        /// Represents a Transact-SQL statement or stored procedure to execute against a SQL Server database 
        /// </summary>
        private SqlCommand command;
        /// <summary>
        /// The string which will hold the connection to the database
        /// </summary>
        private readonly string connectionString;
        /// <summary>
        /// Represents an open connection to a SQL Server database
        /// </summary>
        private SqlConnection connection;
        /// <summary>
        /// This will hold which stored procedure to be used
        /// </summary>
        private string storedProcedureQuery;
        /// <summary>
        /// Provides a way of reading a forward-only stream of rows from a SQL Server database
        /// </summary>
        private SqlDataReader reader;

        public StoredProcedureType storedProcedureType;
        #endregion

        public Access()
        {
            connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ChatProgram; Integrated Security = True; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                connection.Close();
            }
            catch (Exception ex)
            {
                //Logger.LogException(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// A method to be used to check if the user and the corresponding password exists
        /// </summary>
        /// <param name="user">The user trying to log on</param>
        /// <param name="username">the users username</param>
        /// <param name="password">the users password</param>
        /// <returns>The username and password, and tru/false depending on existance</returns>
        public bool CheckForUser(string username, string password)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    reader = null;
                }
            }
        }
    }
}
