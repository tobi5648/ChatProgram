using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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

        public string StoredProcedureQuery
        {
            get
            {
                return storedProcedureQuery;
            }

            set
            {
                storedProcedureQuery = value;
            }
        }
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
            catch (Exception )
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
        public bool CheckForUser(out bool correctPassword, out bool passwordUsed, string username, string password = null)
        {
            if (SetUp(out correctPassword, out passwordUsed, username, password) == true)
                return true;
            return false;
        }

        private bool SetUp(out bool correctPassword, out bool passwordUsed, string username, string password = null)
        {
            string expectedUsername = string.Empty;
            string expectedPassword = string.Empty;
            StoredProcedureQuery = "CheckForUserWithPassword";
            if (password == null)
                StoredProcedureQuery = "CheckForUserWithoutPassword";
            try
            {
                correctPassword = false;
                passwordUsed = false;
                using (connection)
                {
                    connection.Open();
                    reader = null;
                    using (command = new SqlCommand(StoredProcedureQuery, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar)).Value = username;
                        if (password != null)
                            command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar)).Value = password;
                        reader = command.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                expectedUsername = reader["username"].ToString();
                                if (password != null)
                                    expectedPassword = reader["userpassword"].ToString();
                            }
                            if (expectedUsername == username)
                            {
                                if (password != null)
                                    passwordUsed = true;
                                if (expectedPassword == password)
                                    correctPassword = true;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        catch(SqlException) { throw; }
                    }
                }
            }
            catch (ObjectDisposedException) { throw; }
            catch (InvalidOperationException) { throw; }
            catch (SqlException) { throw; }
            catch (System.Configuration.ConfigurationException) { throw; }
            catch (ArgumentNullException) { throw; }
            catch (ArgumentException) { throw; }
            catch (InvalidCastException) { throw; }
            catch (System.IO.IOException) { throw; }
        }
    }
}
