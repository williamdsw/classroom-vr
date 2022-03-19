using System;
using SQLite.Attributes;

namespace MVC.Model
{
    [Serializable, Table("user")]
    public class User
    {
        /// <summary>
        /// Database Autoincrement Id
        /// </summary>
        [AutoIncrement, PrimaryKey, Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// User's name
        /// </summary>
        [NotNull, Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        [NotNull, Column("email")]
        public string Email { get; set; }

        /// <summary>
        /// User's phone
        /// </summary>
        [NotNull, Column("phone")]
        public string Phone { get; set; }
    }
}