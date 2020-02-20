using UnityEngine;
using SwordAndBored.Utilities.Debug;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    public class TimeManagerKeyboardController : MonoBehaviour
    {
        public TimeTrackingTimeManager timeManager;
        public KeyCode nextTurnKey = KeyCode.Return;

#if DEBUG
        void Start()
        {
            AssertHelper.IsSetInEditor(timeManager, this);
        }
#endif


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(nextTurnKey))
            {
                timeManager.AdvanceTimeStep();
            }
        }
    }
}
