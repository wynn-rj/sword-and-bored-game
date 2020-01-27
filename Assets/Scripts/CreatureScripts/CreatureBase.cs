using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBase : UnitBase
{
    [Header("Creature Info")]
    public string creatureName;
    public int maxHealth;
    public int health;

    [HideInInspector]
    public bool action = true;
    [HideInInspector]
    public bool bonus = true;
    [HideInInspector]
    public bool reaction = true;

    public int maxMovement;
    int movement;
    
    void Start()
    {
        health = maxHealth;
        movement = maxMovement;
    }
    
    public void StartTurn()
    {
        action = true;
        bonus = true;
        reaction = true;
        movement = maxMovement;
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(transform.gameObject);
        }    
    }

    public void Damage(int damage)
    {
        health -= damage;
    }
}


