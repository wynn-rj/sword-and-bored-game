namespace SwordAndBored.Strategy.TimeSystem.Subscribers
{
    public abstract class MainThreadPostTimeStepSubscriber : MainThreadGeneralTimeStepSubscriber, IPostTimeStepSubscriber
    {
        public virtual void PostTimeStepUpdate()
        {
            mainThreadActionToCall = MainThreadPostTimeStepUpdate;
            EnterMainThread();
        }

        protected abstract void MainThreadPostTimeStepUpdate();
    }
}
