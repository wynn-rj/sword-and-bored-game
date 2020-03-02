using SwordAndBored.Utilities.Debug;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace SwordAndBored.Utilities.UnityHelper
{
    class UnityMainThreadDispatcher : MonoBehaviour
    {
        public static UnityMainThreadDispatcher Instance { 
            get
            {
                AssertHelper.Assert(instance != null, "No game object present for thread dispatcher");
                return instance;
            }
        }
        private static UnityMainThreadDispatcher instance;
        private static Queue<Action> actionQueue;
        private static readonly object queueLock = new object();

        private void Awake()
        {
            instance = this;
            actionQueue = new Queue<Action>();
        }

        private void Update()
        {
            try
            {
                Monitor.Enter(queueLock);
                while (actionQueue.Count > 0)
                {
                    actionQueue.Dequeue().Invoke();
                }
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogException(exception);
            }
            finally
            {
                Monitor.Exit(queueLock);
            }
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void RunOnMainThread(Action action)
        {
            if (action is null) return;
            try
            {
                Monitor.Enter(queueLock);
                actionQueue.Enqueue(action);
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogException(exception);
            }
            finally
            {
                Monitor.Exit(queueLock);
            }
        }
    }
}
