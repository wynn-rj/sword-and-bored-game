using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.TurnMechanism;

namespace SwordAndBored.Battlefield.CameraUtilities
{
    public class CameraTarget : MonoBehaviour
    {

        public TurnManager manager;

    
        void Start()
        {
            transform.position = manager.activePlayer.transform.position;
        }

    
        void Update()
        {
            transform.position = manager.activePlayer.transform.position;
        }
    }

}
