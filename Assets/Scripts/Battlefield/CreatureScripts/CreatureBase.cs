using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CreaturScripts
{
    public class CreatureBase : UnitBase
    {
        [Header("Creature Info")]
        public string creatureName;
        public int maxHealth;
        public int health;
        public Animator anim;

        [HideInInspector]
        public bool action = true;

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
            if (health <= 0)
            {
                Destroy(transform.gameObject);
            }
        }
    }

}


