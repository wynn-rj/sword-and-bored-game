namespace SwordAndBored.Strategy.TimeSystem.Subscribers
{
    /// <summary>
    /// A subscriber that is notified after a time step ends but before the next one starts
    /// </summary>
    public interface IPostTimeStepSubscriber
    {
        /// <summary>
        /// The function the time manager calls after the time step ends but before the next one starts
        /// </summary>
        void PostTimeStepUpdate();
    }
}
