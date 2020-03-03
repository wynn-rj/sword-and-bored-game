using SwordAndBored.Strategy.TimeSystem.Subscribers;

namespace SwordAndBored.Strategy.GameResources
{
    public abstract class AbstractCity : MainThreadPostTimeStepSubscriber
    {
        public ResourceManager ResourceManager { get; set; }
        public bool UnderPlayerControl { get; set; }
    }
}