using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CreaturScripts
{
    public class UnitStats : MonoBehaviour
    {
        [Header("Player Stats")]
        public int health;
        public int physicalAttack;
        public int magicAttack;
        public int physicalDefense;
        public int magicDefense;
        public int speedIntit;
        public int movement;
        public int accuracy;
        public int evasion;
        public string role;
    }

}
