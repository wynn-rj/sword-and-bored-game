using SwordAndBored.Strategy.TimeSystem.Subscribers;
using UnityEngine;
using UnityEngine.UI;
using SwordAndBored.Utilities.Debug;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    public class TimeDisplayController : MonoBehaviour, IPreTimeStepSubscriber
    {
        public TimeTrackingTimeManager timeManager;

        private Text text;

        void Start()
        {
            text = GetComponent<Text>();
            AssertHelper.IsSetInEditor(timeManager, this);
            timeManager.Subscribe(this);
            text.text = string.Format("Turn: {0}", timeManager.TimeStep);            
        }

        public void PreTimeStepUpdate()
        {
            text.text = string.Format("Turn: {0}", timeManager.TimeStep);
        }

        public void OnDestroy()
        {
            if (timeManager)
            {
                timeManager.Unsubscribe(this);
            }
        }
    }
}

