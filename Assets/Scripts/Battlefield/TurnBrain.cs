using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield
{
    public class TurnBrain : MonoBehaviour
    {
        public AbstractTurnBrain brain;
    
        void Update()
        {
            brain.UpdateBrainHandler();
        }
    }

}
