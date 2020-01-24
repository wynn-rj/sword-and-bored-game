using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueCreature : CreatureBase
{

    public Ability[] abilities;


    private void Start()
    {
        foreach (Ability ability in abilities)
        {
            ability.Initialize(transform.gameObject);
        }
    }

    public void UseAbility(int i, GameObject target)
    {
        abilities[i - 1].TriggerAbility(target);
    }
}
