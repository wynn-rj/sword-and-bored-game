﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CreaturScripts
{
    public class UnitAbilitiesContainer : MonoBehaviour
    {
        [Header("Ability Info")]
        public List<AbstractAbility> abilities = new List<AbstractAbility>();
        UniqueCreature unit;

        void Start()
        {
            unit = GetComponent<UniqueCreature>();
            foreach (AbstractAbility ability in abilities)
            {
                ability.Initialize(transform.gameObject);
            }
        }

        public void UseAbility(int i, GameObject target)
        {
            unit.animator.SetTrigger("Attack");
            abilities[i].TriggerAbility(target);
            unit.action = false;
        }
    }

}
