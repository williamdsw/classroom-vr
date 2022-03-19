using MVC.Model;
using System;

namespace Utilities
{
    /// <summary>
    /// Progress data for Player
    /// </summary>
    [Serializable]
    public class PlayerProgress
    {
        // || State

        private User user;

        // || Properties

        public User User { get; set; }

        public PlayerProgress()
        {
            user = new User();
        }
    }
}