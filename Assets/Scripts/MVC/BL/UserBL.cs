using MVC.DAO;
using MVC.Model;

namespace MVC.BL
{
    /// <summary>
    /// Business Layer for User
    /// </summary>
    public class UserBL
    {
        private UserDAO dao = new UserDAO();

        /// <summary>
        /// Insert user's data
        /// </summary>
        /// <param name="user"> User model </param>
        public bool Insert(User user) => dao.Insert(user);

        /// <summary>
        /// Update user's data
        /// </summary>
        /// <param name="user"> User model </param>
        public bool Update(User user) => dao.Update(user);

        /// <summary>
        /// Find unique user
        /// </summary>
        public User FindUnique() => dao.FindUnique();
    }
}