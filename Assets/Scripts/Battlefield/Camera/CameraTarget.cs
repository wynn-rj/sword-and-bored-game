using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.TurnMechanism;

namespace SwordAndBored.Battlefield.CameraUtilities
{
    public class CameraTarget : MonoBehaviour
    {

        public Transform player;
    
        void Start()
        {
            transform.parent = null;
            transform.position = player.position;
        }

    
        void Update()
        {
            transform.position = player.position;
        }
    }

}
