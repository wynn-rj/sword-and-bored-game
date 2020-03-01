using SwordAndBored.Strategy.TimeSystem.Subscribers;
using SwordAndBored.Strategy.Transitions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    public class TimeTrackingTimeManager : AbstractTimeManager
    {
        protected override IList<IPreTimeStepSubscriber> PreTimeStepSubscribers { get; set; }
        protected override IList<IPostTimeStepSubscriber> PostTimeStepSubscribers { get; set; }

        [UnityEngine.SerializeField] private ulong startingTimeStep = 0;
        private readonly object timeStepLock = new object();

        public TimeTrackingTimeManager()
        {
            TimeStep = (SceneSharing.useStoredTimeStep) ? SceneSharing.timeStep : startingTimeStep;
            PreTimeStepSubscribers = new List<IPreTimeStepSubscriber>();
            PostTimeStepSubscribers = new List<IPostTimeStepSubscriber>();
        }

        public override void AdvanceTimeStep()
        {
            Task.Run(AsyncAdvanceTimeStep);
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

        private void AsyncAdvanceTimeStep()
        {
            if (!Monitor.TryEnter(timeStepLock))
            {
                return;
            }
            try
            {
                List<Task> tasks = new List<Task>();
                foreach (IPostTimeStepSubscriber postTimeStepSubscriber in PostTimeStepSubscribers)
                {
                    tasks.Add(Task.Run(postTimeStepSubscriber.PostTimeStepUpdate));
                }
                Task.WaitAll(tasks.ToArray());

                TimeStep++;

                tasks.Clear();
                foreach (IPreTimeStepSubscriber preTimeStepSubscriber in PreTimeStepSubscribers)
                {
                    tasks.Add(Task.Run(preTimeStepSubscriber.PreTimeStepUpdate));
                }
                Task.WaitAll(tasks.ToArray());
            }
            finally
            {
                Monitor.Exit(timeStepLock);
            }
        }
    }
}
