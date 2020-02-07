using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CreaturScripts
{
    public class UnitAbilitiesContainer : MonoBehaviour
    {
        [Header("Ability Info")]
        public AAbilitySO[] abilities;
        UniqueCreature unit;

        void Start()
        {
            unit = GetComponent<UniqueCreature>();
            foreach (AAbilitySO ability in abilities)
            {
                ability.Initialize(transform.gameObject);
            }
        }

        public void UseAbility(int i, GameObject target)
        {
            unit.animator.SetTrigger("Attack");
            switch (abilities[i].TypeOfActionRequired)
            {
                case AAbilitySO.ActionTypes.Action:
                    if (unit.action)
                    {
                        abilities[i].TriggerAbility(target);
                        unit.action = false;
                    }
                    break;
                case AAbilitySO.ActionTypes.BonusAction:
                    if (unit.bonus)
                    {
                        abilities[i].TriggerAbility(target);
                        unit.bonus = false;
                    }
                    break;
                case AAbilitySO.ActionTypes.Reaction:
                    if (unit.reaction)
                    {
                        abilities[i].TriggerAbility(target);
                        unit.reaction = false;
                    }
                    break;
            }
        }
    }

}
