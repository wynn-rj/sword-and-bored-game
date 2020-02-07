using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.Battlefield.CameraUtilities;


namespace SwordAndBored.Battlefield
{
    public abstract class AbstractTurnBrain : MonoBehaviour
    {
        public TurnManager turnManager;
        abstract public void UpdateBrainHandler();
    }
}
