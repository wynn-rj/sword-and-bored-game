using SwordAndBored.Utilities;
using SwordAndBored.Strategy.TimeSystem.Subscribers;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    public interface ITimeManager : IObserver<ITimeStepSubscriber>, IObserver<IPreTimeStepSubscriber>, IObserver<IPostTimeStepSubscriber>
    {
        void AdvanceTimeStep();
    }
}
