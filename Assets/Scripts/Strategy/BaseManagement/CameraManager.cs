using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class CameraManager : MonoBehaviour
    {
        public Transform StagingArea;

        public Transform DefaultPosition;

        private void Start()
        {
            UnFocusOnModel();
        }

        public void FocusOnModel()
        {
            transform.position = StagingArea.transform.position;
        }

        public void UnFocusOnModel()
        {
            transform.position = DefaultPosition.transform.position;
        }
    }
}
