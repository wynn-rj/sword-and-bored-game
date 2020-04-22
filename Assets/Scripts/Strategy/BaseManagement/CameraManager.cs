using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform stagingArea;
        [SerializeField] private Transform defaultPosition;

        private void Start()
        {
            FocusOnModel();
        }

        public void FocusOnModel()
        {
            transform.position = stagingArea.transform.position;
        }

        public void UnFocusOnModel()
        {
            transform.position = defaultPosition.transform.position;
        }
    }
}
