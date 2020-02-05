using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Abilities/MeleeAttack", order = 1)]
public class MeleeAttackAbilitySO : AAbilitySO
{

    public int damageDice;
    public int range;
    UniqueCreature user;

    public override void Initialize(GameObject obj)
    {
        user = obj.GetComponent<UniqueCreature>();
    }

    public override void TriggerAbility(GameObject target)
    {
        UniqueCreature enemy = target.GetComponent<UniqueCreature>();
        if (true)
        {
            enemy.Damage(Roll(damageDice));
            //Debug.Log("Hit");
        } else
        {
            //Debug.Log("Miss");
        }
    }
    
}
