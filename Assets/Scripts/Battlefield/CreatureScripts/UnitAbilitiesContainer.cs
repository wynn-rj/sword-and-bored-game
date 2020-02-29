using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CreaturScripts
{
    public class UnitAbilitiesContainer : MonoBehaviour
    {
        [Header("Ability Info")]
        public List<Ability> abilities = new List<Ability>();
        UniqueCreature unit;

        void Start()
        {
            unit = GetComponent<UniqueCreature>();
            foreach (AbstractAbility ability in abilities)
            {
                ability.Initialize(transform.gameObject);
            }
        }

        public void UseAbility(int i, RaycastHit target)
        {
            unit.animator.SetTrigger("Attack");
            abilities[i].TriggerAbility(target);
            Debug.Log(abilities[i].AttackName);
            unit.action = false;
        }
    }

}
