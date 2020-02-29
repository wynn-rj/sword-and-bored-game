using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.Battlefield.CreaturScripts {
    public class Ability : AbstractAbility
    {
        
        public Ability(IAbility abilityStats)
        {
            name = abilityStats.Name;
            damage = abilityStats.Damage;
            range = abilityStats.Range;
            accuraccy = abilityStats.Accuracy;
        }


        bool aoe = false;
        int damage = 10;
        int range;
        int accuraccy;

        UniqueCreature user;

        public override void Initialize(GameObject obj)
        {
            user = obj.GetComponent<UniqueCreature>();
            AttackName = "Spahggetti";
        }

        public override void TriggerAbility(RaycastHit hit)
        {

            if (!aoe)
            {
                pointAttack(hit);
            }
        }

        void pointAttack(RaycastHit hit)
        {
            GameObject target = hit.collider.gameObject;
            UniqueCreature enemy = target.GetComponent<UniqueCreature>();
            if (enemy)
            {
                enemy.hightlight();
            }

            if (true)
            {
                enemy.Damage(damage);
                //Debug.Log("Hit");
            }
            else
            {
                //Debug.Log("Miss");
            }
        }


    
    }
}
