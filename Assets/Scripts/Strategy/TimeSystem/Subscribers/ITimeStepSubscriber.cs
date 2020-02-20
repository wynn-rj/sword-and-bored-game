namespace SwordAndBored.Strategy.TimeSystem.Subscribers
{
    /// <summary>
    /// A subscriber that is notified when a time step ends and a new one starts
    /// </summary>
    public interface ITimeStepSubscriber : IPostTimeStepSubscriber, IPreTimeStepSubscriber
    {
    }
}
