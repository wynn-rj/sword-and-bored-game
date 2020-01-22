using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBase : UnitBase
{
    public string creatureName;
    public int maxHealth;
    int health;

    bool Action = true;
    bool Bonus = true;
    bool Reaction = true;

    public int maxMovement;
    int movement;
    
    void Start()
    {
        health = maxHealth;
        movement = maxMovement;
    }
    
    public void StartTurn()
    {
        Action = true;
        Bonus = true;
        Reaction = true;
        movement = maxMovement;
    }
}
