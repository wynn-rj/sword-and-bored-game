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

        void Awake()
        {
            text = GetComponent<Text>();
            AssertHelper.IsSetInEditor(timeManager, this);
            AssertHelper.IsSetInEditor(text, this);
               
        }

        void Start()
        {
            timeManager.Subscribe(this);
            PreTimeStepUpdate();
        }        

        public void OnDestroy()
        {
            if (timeManager)
            {
                timeManager.Unsubscribe(this);
            }
        }

        public void PreTimeStepUpdate()
        {
            text.text = string.Format("Turn: {0}", timeManager.TimeStep);
        }
    }
}

