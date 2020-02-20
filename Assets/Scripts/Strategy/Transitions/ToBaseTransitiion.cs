using UnityEngine;
using UnityEngine.SceneManagement;
using SwordAndBored.Strategy.TimeSystem.TimeManager;
using SwordAndBored.Utilities.Debug;

namespace SwordAndBored.Strategy.Transitions
{
    class ToBaseTransitiion : MonoBehaviour
    {
        public ITimeManager timeManager;

#if DEBUG
        void Awake()
        {
            AssertHelper.IsSetInEditor(timeManager, this);
        }
#endif

        private void GoToBase()
        {
            SceneSharing.timeStep = timeManager.TimeStep;
            SceneManager.LoadSceneAsync("BaseManagement", LoadSceneMode.Single);
        }
    }
}
