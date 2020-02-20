using SwordAndBored.Strategy.TimeSystem.Subscribers;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    abstract class AbstractTimeManager : ITimeManager
    {
        protected readonly IList<IPreTimeStepSubscriber> preTimeStepSubscribers = new List<IPreTimeStepSubscriber>();
        protected readonly IList<IPostTimeStepSubscriber> postTimeStepSubscribers = new List<IPostTimeStepSubscriber>();

        public virtual void AdvanceTimeStep()
        {
            foreach (IPostTimeStepSubscriber postTimeStepSubscriber in postTimeStepSubscribers)
            {
                postTimeStepSubscriber.PostTimeStepUpdate();
            }

            foreach (IPreTimeStepSubscriber preTimeStepSubscriber in preTimeStepSubscribers)
            {
                preTimeStepSubscriber.PreTimeStepUpdate();
            }
        }

        public void Subscribe(ITimeStepSubscriber subscriber)
        {
            Subscribe(subscriber as IPreTimeStepSubscriber);
            Subscribe(subscriber as IPostTimeStepSubscriber);
        }

        public bool Unsubscribe(ITimeStepSubscriber subscriber)
        {
            bool success = Unsubscribe(subscriber as IPreTimeStepSubscriber);
            return success && Unsubscribe(subscriber as IPostTimeStepSubscriber);
        }

        public void Subscribe(IPreTimeStepSubscriber subscriber) => preTimeStepSubscribers.Add(subscriber);

        public bool Unsubscribe(IPreTimeStepSubscriber subscriber) => preTimeStepSubscribers.Remove(subscriber);

        public void Subscribe(IPostTimeStepSubscriber subscriber) => postTimeStepSubscribers.Add(subscriber);

        public bool Unsubscribe(IPostTimeStepSubscriber subscriber) => postTimeStepSubscribers.Remove(subscriber);
    }
}
