using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueCreature : CreatureBase
{
    [Header("Ability Info")]
    public Ability[] abilities;
    public int AC;
    public int hitMod;
    public Material[] mat;
    Renderer currentMat;


    void Start()
    {
        foreach (Ability ability in abilities)
        {
            ability.Initialize(transform.gameObject);
        }

        health = maxHealth;

        currentMat = GetComponent<Renderer>();
    }

    public void UseAbility(int i, GameObject target)
    {
        switch (abilities[i].TypeOfActionRequired)
        {
            case Ability.ActionTypes.Action:
                if (action)
                {
                    abilities[i].TriggerAbility(target);
                    action = false;
                }
                break;
            case Ability.ActionTypes.BonusAction:
                if (bonus)
                {
                    abilities[i].TriggerAbility(target);
                    bonus = false;
                }
                break;
            case Ability.ActionTypes.Reaction:
                if (reaction)
                {
                    abilities[i].TriggerAbility(target);
                    reaction = false;
                }
                break;
        }
    }

    public void Glow(bool glow)
    {
        if (glow)
        {
            currentMat.material = mat[1];
        } else
        {
            currentMat.material = mat[0];
        }
    }
}
