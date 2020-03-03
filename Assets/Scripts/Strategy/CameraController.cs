using UnityEngine;
using SwordAndBored.Strategy.Transitions;

namespace SwordAndBored.Strategy
{
    public class CameraController : MonoBehaviour
    {
        public float speed;
        public int boundary;

        readonly float ysensitivity = 15f;
        readonly float zsensitivity = 10f;

        void Start()
        {
            if (SceneSharing.cameraPosition != default)
            {
                this.transform.position = SceneSharing.cameraPosition;
            }
        }

        void Update()
        {
            Vector3 zoom = new Vector3(transform.position.x, transform.position.y - (Input.GetAxis("Mouse ScrollWheel") * ysensitivity), transform.position.z + (Input.GetAxis("Mouse ScrollWheel") * zsensitivity));
            transform.position = zoom;

            Vector3 move = transform.position;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                move.x += speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                move.x -= speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                move.z += speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                move.z -= speed * Time.deltaTime;
            }
            transform.position = move;
        }

        private void OnDestroy()
        {
            SceneSharing.cameraPosition = this.transform.position;
        }
    }
}