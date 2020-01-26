using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBase : UnitBase
{
    public string creatureName;
    public int maxHealth;
    public int health;

    bool action = true;
    bool bonus = true;
    bool reaction = true;

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
}


