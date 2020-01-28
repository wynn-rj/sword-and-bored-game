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
    float a = .05f;
    float b;
    int highlightColor;

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
        anim.SetTrigger("Attack");
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

    public void Glow(int glow)
    {
        highlightColor = glow;
        if (glow == 1)
        {
            currentMat.material = mat[1];
        } else if (glow == 2)
        {
            currentMat.material = mat[0];
        } else if (glow == 3) 
        {
            b = a + Time.time;
            currentMat.material = mat[2];
        }
    }

    private void Update()
    {
        if (highlightColor == 3 && Time.time > b)
        {
            Glow(2);
        }
    }
}
