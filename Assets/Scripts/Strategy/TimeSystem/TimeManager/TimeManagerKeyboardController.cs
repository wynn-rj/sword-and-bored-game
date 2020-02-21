using UnityEngine;
using SwordAndBored.Utilities.Debug;

namespace SwordAndBored.Strategy.TimeSystem.TimeManager
{
    public class TimeManagerKeyboardController : MonoBehaviour
    {
        public AbstractTimeManager timeManager;
        public KeyCode nextTurnKey = KeyCode.Return;

#if DEBUG
        void Awake()
        {
            AssertHelper.IsSetInEditor(timeManager, this);
        }
#endif

        void Update()
        {
            if (Input.GetKeyDown(nextTurnKey))
            {
                timeManager.AdvanceTimeStep();
            }
        }
    }
}
