using System;
using System.Threading;
using UnityEngine;
using SwordAndBored.Utilities.UnityHelper;

namespace SwordAndBored.Strategy.TimeSystem.Subscribers
{
    public abstract class MainThreadGeneralTimeStepSubscriber : MonoBehaviour
    {
        protected Action mainThreadActionToCall;
        private readonly Semaphore mainThreadExitControl = new Semaphore(0, 1);

        protected void EnterMainThread()
        {
            try
            {
                UnityMainThreadDispatcher.Instance.RunOnMainThread(LockedMainThreadUpdate);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
            finally
            {
                mainThreadExitControl.WaitOne();
            }
        }

        private void LockedMainThreadUpdate()
        {
            try
            {
                mainThreadActionToCall?.Invoke();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
            finally
            {
                mainThreadExitControl.Release();
            }
        }
    }
}
