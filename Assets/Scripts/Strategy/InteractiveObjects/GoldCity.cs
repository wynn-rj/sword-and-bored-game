using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Strategy.GameResources;

namespace SwordAndBored.Strategy.InteractiveObjects
{
    public class GoldCity : MonoBehaviour, IInteractiveObject
    {
        public Gold gold;

        public void addGold()
        {
            gold.Amount += 100;
        }

        //collision detection in here or on player to call addGold() method
        //This class can be reworked to add other resource/item additions if needed
    }
}
