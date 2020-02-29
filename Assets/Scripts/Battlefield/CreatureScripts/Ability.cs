using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.Battlefield.CreaturScripts {
    public class Ability : AbstractAbility
    {
        /*
        public Ability(CombatAbilities abilityStats)
        {

        }*/



        int damage = 10;
        int range;
        int accuraccy;

        UniqueCreature user;

        public override void Initialize(GameObject obj)
        {
            user = obj.GetComponent<UniqueCreature>();
            AttackName = "Spahggetti";
        }

        public override void TriggerAbility(GameObject target)
        {
            UniqueCreature enemy = target.GetComponent<UniqueCreature>();
            if (true)
            {
                enemy.Damage(damage);
                //Debug.Log("Hit");
            } else
            {
                //Debug.Log("Miss");
            }
        }
    
    }
}
