using System;

namespace Utilities
{
    /// <summary>
    /// Formatter util
    /// </summary>
    public class Formatter
    {
        /// <summary>
        /// Get time properties by timer count
        /// </summary>
        /// <param name="timer"> Timer count </param>
        /// <param name="hours"> Hours to be calculated </param>
        /// <param name="minutes"> Minutes to be calculated </param>
        /// <param name="seconds"> Seconds to be calculated </param>
        private static void GetTime(int timer, out int hours, out int minutes, out int seconds)
        {
            hours = (timer / 3600);
            minutes = (timer - (hours * 3600)) / 60;
            seconds = (timer - (hours * 3600) - (minutes * 60));
        }

        /// <summary>
        /// Get formatted ellapsed time in hours. Ex: "01:32:05"
        /// </summary>
        /// <param name="timer"> Timer count </param>
        /// <returns> Formatted ellapsed time </returns>
        public static string GetEllapsedTimeInHours(int timer)
        {
            int hours, minutes, seconds;
            GetTime(timer, out hours, out minutes, out seconds);
            return string.Format("{0}:{1}:{2}", hours.ToString("00"), minutes.ToString("00"), seconds.ToString("00"));
        }

        /// <summary>
        /// Get formatted ellapsed time in minutes. Ex: "10:32"
        /// </summary>
        /// <param name="timer"> Timer count </param>
        /// <returns> Formatted ellapsed time </returns>
        public static string GetEllapsedTimeInMinutes(int timer)
        {
            int hours, minutes, seconds;
            GetTime(timer, out hours, out minutes, out seconds);
            return string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));
        }
    }
}