using SwordAndBored.Strategy.TimeSystem.Subscribers;
using UnityEngine;
using UnityEngine.UI;
using SwordAndBored.Utilities.Debug;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    public class TimeDisplayController : MainThreadPreTimeStepSubscriber
    {
        [SerializeField] private AbstractTimeManager timeManager;
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
            MainThreadPreTimeStepUpdate();
        }

        public void OnDestroy()
        {
            if (!(timeManager is null))
            {
                timeManager.Unsubscribe(this);
            }
        }

        protected override void MainThreadPreTimeStepUpdate()
        {
            text.text = string.Format("Turn: {0}", timeManager.TimeStep);
        }
    }
}

