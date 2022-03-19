using System.Text.RegularExpressions;

namespace Utilities
{
    public class Validators
    {
        /// <summary>
        /// Check if given email is valid
        /// </summary>
        /// <param name="email"> Email to be validated </param>
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// Check if given phone is valid
        /// </summary>
        /// <param name="phone"> Phone to be validated </param>
        public static bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^(\(11\) [9][0-9]{4}-[0-9]{4})|(\(1[2-9]\) [5-9][0-9]{3}-[0-9]{4})|(\([2-9][1-9]\) [5-9][0-9]{3}-[0-9]{4})");
        }
    }
}
