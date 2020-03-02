using SwordAndBored.Strategy.TimeSystem.Subscribers;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    public abstract class AbstractTimeManager : MonoBehaviour, ITimeManager
    {
        [SerializeField] private bool isTimeStepAdvancing;

        public ulong TimeStep { get; protected set; }

        public bool IsTimeStepAdvancing { get => isTimeStepAdvancing; protected set => isTimeStepAdvancing = value; }

        protected abstract IList<IPreTimeStepSubscriber> PreTimeStepSubscribers { get; set; }
        protected abstract IList<IPostTimeStepSubscriber> PostTimeStepSubscribers { get; set; }

        public virtual void AdvanceTimeStep()
        {
            foreach (IPostTimeStepSubscriber postTimeStepSubscriber in PostTimeStepSubscribers)
            {
                postTimeStepSubscriber.PostTimeStepUpdate();
            }

            foreach (IPreTimeStepSubscriber preTimeStepSubscriber in PreTimeStepSubscribers)
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

        public void Subscribe(IPreTimeStepSubscriber subscriber) => PreTimeStepSubscribers.Add(subscriber);

        public bool Unsubscribe(IPreTimeStepSubscriber subscriber) => PreTimeStepSubscribers.Remove(subscriber);

        public void Subscribe(IPostTimeStepSubscriber subscriber) => PostTimeStepSubscribers.Add(subscriber);

        public bool Unsubscribe(IPostTimeStepSubscriber subscriber) => PostTimeStepSubscribers.Remove(subscriber);
    }
}
