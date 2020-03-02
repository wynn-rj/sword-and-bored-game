namespace SwordAndBored.Strategy.TimeSystem.Subscribers
{
    public abstract class MainThreadTimeStepSubscriber : MainThreadGeneralTimeStepSubscriber, ITimeStepSubscriber
    {
        /*
         * NOTE: PreTimeStepUpdate and PostTimeStepUpdate can never run at the same time and are thread safe so
         * it should never be an issue that it is just switching the value of mainThreadAction
         */

        public virtual void PreTimeStepUpdate()
        {
            mainThreadActionToCall = MainThreadPreTimeStepUpdate;
            EnterMainThread();
        }

        protected abstract void MainThreadPreTimeStepUpdate();

        public virtual void PostTimeStepUpdate()
        {
            mainThreadActionToCall = MainThreadPostTimeStepUpdate;
            EnterMainThread();
        }

        protected abstract void MainThreadPostTimeStepUpdate();
    }
}
