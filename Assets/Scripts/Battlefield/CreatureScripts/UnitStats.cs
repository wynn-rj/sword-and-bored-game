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

        public int physicalAttackMax;
        public int magicAttackMax;
        public int physicalDefenseMax;
        public int magicDefenseMax;
        public int speedIntitMax;
        public int movementMax;

        public string role;

        public bool IsBurning;
        public bool IsFrozen;
        public bool IsBleeding;
        public bool IsStunned;

        public int BurnResist;
        public int BleedResist;
        public int StunResist;
        public int FreezeResist;

        public bool HasStatus()
        {
            return IsBleeding || IsBurning || IsFrozen || IsStunned;
        }
    }

}
