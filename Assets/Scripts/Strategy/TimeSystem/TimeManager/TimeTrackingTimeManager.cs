using SwordAndBored.Strategy.TimeSystem.Subscribers;
using SwordAndBored.Strategy.Transitions;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    public class TimeTrackingTimeManager : AbstractTimeManager
    {
        protected override IList<IPreTimeStepSubscriber> PreTimeStepSubscribers { get; set; }
        protected override IList<IPostTimeStepSubscriber> PostTimeStepSubscribers { get; set; }

        public ulong startingTimeStep = 0;

        public TimeTrackingTimeManager()
        {
            TimeStep = (SceneSharing.useStoredTimeStep) ? SceneSharing.timeStep : startingTimeStep;
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

        public void AddPostTimeStepSubscriber(IPostTimeStepSubscriber postTimeStepSubscriber)
        {
            PostTimeStepSubscribers.Add(postTimeStepSubscriber);
        }

        public void OnDestroy()
        {
            SceneSharing.timeStep = TimeStep;
            SceneSharing.useStoredTimeStep = true;
        }
    }
}
