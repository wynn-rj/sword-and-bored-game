using SwordAndBored.Utilities.UnityHelper;
using SwordAndBored.Strategy.TimeSystem.Subscribers;
using SwordAndBored.Strategy.Transitions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using SwordAndBored.GameData;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    public class TimeTrackingTimeManager : AbstractTimeManager
    {
        protected override IList<IPreTimeStepSubscriber> PreTimeStepSubscribers { get; set; }
        protected override IList<IPostTimeStepSubscriber> PostTimeStepSubscribers { get; set; }

        [UnityEngine.SerializeField] private ulong startingTimeStep = 0;
        private readonly object timeStepLock = new object();

        private TimeTrackingTimeManager()
        {            
            PreTimeStepSubscribers = new List<IPreTimeStepSubscriber>();
            PostTimeStepSubscribers = new List<IPostTimeStepSubscriber>();
            IsTimeStepAdvancing = false;
        }

        public override void AdvanceTimeStep()
        {
            Task.Run(AsyncAdvanceTimeStep);
        }

        public void AddPostTimeStepSubscriber(IPostTimeStepSubscriber postTimeStepSubscriber)
        {
            PostTimeStepSubscribers.Add(postTimeStepSubscriber);
        }

        private void Awake()
        {
            TimeStep = (ulong)ResourceHelper.GetTurnNumber();
        }

        void OnDestroy()
        {
            if (((ulong)(int)TimeStep) != TimeStep)
            {
                Debug.LogError("INTEGER OVERFLOW ON TURN");
            }
            ResourceHelper.SetTurnNumber((int)TimeStep);
        }

        private void AsyncAdvanceTimeStep()
        {
            if (!Monitor.TryEnter(timeStepLock))
            {
                return;
            }
            try
            {
                IsTimeStepAdvancing = true;
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
            catch (AggregateException exceptions)
            {
                Debug.LogWarning("Exception(s) detected during advancing of time step");
                exceptions.Handle((x) => 
                {
                    Debug.LogException(x);
                    return true;
                }) ;
                Debug.Log("End of advancing of time step exceptions");
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
            finally
            {
                IsTimeStepAdvancing = false;
                Monitor.Exit(timeStepLock);
            }
        }
    }
}
