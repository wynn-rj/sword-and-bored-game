using SwordAndBored.Strategy.TimeSystem.Subscribers;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    public class TimeTrackingTimeManager : AbstractTimeManager
    {
        public int TimeStep { get; protected set; }
        protected override IList<IPreTimeStepSubscriber> PreTimeStepSubscribers { get; set; }
        protected override IList<IPostTimeStepSubscriber> PostTimeStepSubscribers { get; set; }

        public int startingTimeStep = 0;

        public void Awake()
        {
            TimeStep = startingTimeStep;
            PreTimeStepSubscribers = new List<IPreTimeStepSubscriber>();
            PostTimeStepSubscribers = new List<IPostTimeStepSubscriber>();
        }

        public override void AdvanceTimeStep()
        {
            foreach (IPostTimeStepSubscriber postTimeStepSubscriber in PostTimeStepSubscribers)
            {
                postTimeStepSubscriber.PostTimeStepUpdate();
            }

            TimeStep++;

            foreach (IPreTimeStepSubscriber preTimeStepSubscriber in PreTimeStepSubscribers)
            {
                preTimeStepSubscriber.PreTimeStepUpdate();
            }
        }
    }
}
