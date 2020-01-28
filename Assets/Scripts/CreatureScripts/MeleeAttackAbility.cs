using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Abilities/MeleeAttack", order = 1)]
public class MeleeAttackAbility : Ability
{

    public int damageDice;
    int hitMod;
    public int range;
    UniqueCreature user;

    public override void Initialize(GameObject obj)
    {
        user = obj.GetComponent<UniqueCreature>();
        hitMod = user.hitMod;
    }

    public override void TriggerAbility(GameObject target)
    {
        UniqueCreature enemy = target.GetComponent<UniqueCreature>();
        int toHit = Roll() + hitMod;
        if (toHit > enemy.AC)
        {
            enemy.Damage(Roll(damageDice));
            Debug.Log("Hit");
        } else
        {
            Debug.Log("Miss");
        }
    }
    
}
