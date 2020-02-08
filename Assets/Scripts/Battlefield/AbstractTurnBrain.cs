using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.Battlefield.CameraUtilities;
using Cinemachine;


namespace SwordAndBored.Battlefield
{
    public abstract class AbstractTurnBrain : MonoBehaviour
    {
        public abstract void DoTurn();
        public abstract string GetName();
        public abstract bool GetTurnEnd();
        public abstract CinemachineVirtualCamera GetCam();
        public abstract bool HasActionLeft();
    }
}
