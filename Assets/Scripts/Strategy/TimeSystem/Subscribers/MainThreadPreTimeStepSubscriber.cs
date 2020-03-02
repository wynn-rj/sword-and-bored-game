namespace SwordAndBored.Strategy.TimeSystem.Subscribers
{
    public abstract class MainThreadPreTimeStepSubscriber : MainThreadGeneralTimeStepSubscriber, IPreTimeStepSubscriber
    {
        public virtual void PreTimeStepUpdate()
        {
            mainThreadActionToCall = MainThreadPreTimeStepUpdate;
            EnterMainThread();
        }

        protected abstract void MainThreadPreTimeStepUpdate();
    }
}
