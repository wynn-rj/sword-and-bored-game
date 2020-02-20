using SwordAndBored.Utilities;
using SwordAndBored.Strategy.TimeSystem.Subscribers;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    interface ITimeManager : IObserver<ITimeStepSubscriber>, IObserver<IPreTimeStepSubscriber>, IObserver<IPostTimeStepSubscriber>
    {
        void AdvanceTimeStep();
    }
}
