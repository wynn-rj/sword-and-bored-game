namespace SwordAndBored.Strategy.TimeSystem.Subscribers
{
    /// <summary>
    /// A subscriber that is notified when a new time step starts
    /// </summary>
    public interface IPreTimeStepSubscriber
    {
        /// <summary>
        /// The function the time manager calls when a new time step starts
        /// </summary>
        void PreTimeStepUpdate();
    }
}