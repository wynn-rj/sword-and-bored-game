using SwordAndBored.Strategy.TimeSystem.Subscribers;
using UnityEngine;
using UnityEngine.UI;
using SwordAndBored.Utilities.Debug;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    public class TimeDisplayController : MonoBehaviour, IPreTimeStepSubscriber
    {
        [SerializeField] private AbstractTimeManager timeManager;
        private Text text;
        private string turnString;
        private volatile bool changeText;

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

        void Update()
        {
            if (changeText)
            {
                text.text = turnString;
                changeText = false;
            }
        }

        public void OnDestroy()
        {
            if (!(timeManager is null))
            {
                timeManager.Unsubscribe(this);
            }
        }

        public void PreTimeStepUpdate()
        {
            turnString = string.Format("Turn: {0}", timeManager.TimeStep);
            changeText = true;
        }
    }
}

