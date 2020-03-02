using SwordAndBored.Utilities;
using SwordAndBored.Strategy.TimeSystem.Subscribers;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    /// <summary>
    /// An observer that notifies subscribers when a time step ends and a new time step starts
    /// </summary>
    public interface ITimeManager : IObserver<ITimeStepSubscriber>, IObserver<IPreTimeStepSubscriber>, IObserver<IPostTimeStepSubscriber>
    {
        /// <summary>
        /// The current time step
        /// </summary>
        ulong TimeStep { get; }

        /// <summary>
        /// Returns true if AdvanceTimeStep is currently running
        /// </summary>
        bool IsTimeStepAdvancing { get; }

        /// <summary>
        /// End the current time step and go to the next one
        /// </summary>
        void AdvanceTimeStep();
    }
}
