using SqlCipher4Unity3D;
using Utilities;

namespace MVC.Global
{
    /// <summary>
    /// SQLite Connection class
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Current connection instance
        /// </summary>
        protected SQLiteConnection MyConnection { get; private set; }

        /// <summary>
        /// Open a new connection
        /// </summary>
        protected void OpenConnection() => MyConnection = new SQLiteConnection(Properties.DatabasePath, Properties.DatabasePassword);

        /// <summary>
        /// Executes a sql command
        /// </summary>
        /// <param name="query"> SQL query to be executed </param>
        protected void ExecuteCommand(string query)
        {
            if (MyConnection != null)
            {
                SQLiteCommand command = MyConnection.CreateCommand(query);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Close current connection
        /// </summary>
        protected void CloseConnection()
        {
            if (MyConnection != null)
            {
                MyConnection.Close();
            }
        }
    }
}