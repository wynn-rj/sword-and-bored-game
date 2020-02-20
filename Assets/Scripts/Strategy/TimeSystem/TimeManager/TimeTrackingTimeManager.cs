using SwordAndBored.Strategy.TimeSystem.Subscribers;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    class TimeTrackingTimeManager : AbstractTimeManager
    {
        public int TimeStep { get; protected set; } = 0;

        public override void AdvanceTimeStep()
        {
            foreach (IPostTimeStepSubscriber postTimeStepSubscriber in postTimeStepSubscribers)
            {
                postTimeStepSubscriber.PostTimeStepUpdate();
            }

            TimeStep++;

            foreach (IPreTimeStepSubscriber preTimeStepSubscriber in preTimeStepSubscribers)
            {
                preTimeStepSubscriber.PreTimeStepUpdate();
            }
        }
    }
}
