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


        public bool aoe = false;
        public int damage;
        public int range;
        public int accuraccy;

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
            if (Vector3.Distance(user.transform.position, hit.point) <= range)
            {
                UniqueCreature enemy = getEnemy(hit);
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

        public override void ShowTarget(RaycastHit hit)
        {
            if (Vector3.Distance(user.transform.position, hit.point) <= range)
            {
                UniqueCreature enem = getEnemy(hit);
                if (enem && enem != user)
                {
                    enem.hightlight();
                }
            }
        }

        UniqueCreature getEnemy(RaycastHit hit)
        {
            GameObject target = hit.collider.gameObject;
            UniqueCreature enem = target.GetComponent<UniqueCreature>();
            return enem;
        }
    }
}
