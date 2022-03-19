using MVC.Global;
using MVC.Model;
using System;

namespace MVC.DAO
{
    /// <summary>
    /// Database operations for Character
    /// </summary>
    public class UserDAO : Connection
    {
        public UserDAO() => CreateTable();

        /// <summary>
        /// Create User table if not exists
        /// </summary>
        private void CreateTable()
        {
            try
            {
                OpenConnection();
                MyConnection.CreateTable<User>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Insert user's data
        /// </summary>
        /// <param name="user"> User model </param>
        public bool Insert(User user)
        {
            try
            {
                OpenConnection();
                return MyConnection.Insert(user) == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Update user's data
        /// </summary>
        /// <param name="user"> User model </param>
        public bool Update(User user)
        {
            try
            {
                OpenConnection();
                return MyConnection.Update(user) == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Find unique user
        /// </summary>
        public User FindUnique()
        {
            try
            {
                OpenConnection();
                return MyConnection.Table<User>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}