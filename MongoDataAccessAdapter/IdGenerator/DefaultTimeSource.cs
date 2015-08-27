using System;

namespace MongoDataAccessAdapter
{
    /// <summary>
    /// Provides time data to an <see cref="IdGenerator"/>. Uses the current date and time on this computer.
    /// </summary>
    public class DefaultTimeSource 
    {
        /// <summary>
        /// Returns a <see cref="DateTime"/> object that is set to the current date and time on this computer, expressed
        /// as the Coordinated Universal Time (UTC).
        /// </summary>
        /// <returns>
        /// A <see cref="DateTime"/> object that is set to the current date and time on this computer, expressed as the
        /// Coordinated Universal Time (UTC).
        /// </returns>
        /// <remarks>
        /// The resolution of this value depends on the system timer.
        /// </remarks>
        public DateTime GetTime()
        {
            return DateTime.UtcNow;
        }
    }
}
