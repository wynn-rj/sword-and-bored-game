using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CreaturScripts
{
    public class UnitStats : MonoBehaviour
    {
        [Header("Player Stats")]
        public int health;
        public int maxHealth;
        public int physicalAttack;
        public int magicAttack;
        public int physicalDefense;
        public int magicDefense;
        public int speedIntit;
        public int movement;
        public string role;
    }

}
