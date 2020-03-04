using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SwordAndBored.UI.Battlefield;
using UnityEngine.UI;


namespace SwordAndBored.Battlefield.CreaturScripts
{

    public class UnitBase : MonoBehaviour
    {

        [Header("Creature Info")]
        public string creatureName;
        public int maxHealth;
        public int health;
        public Animator anim;

        [HideInInspector]
        public bool action = true;

        public int maxMovement;
        public int movementLeft;

        void Start()
        {
            health = maxHealth;
            movementLeft = maxMovement;
            movementLeft = gameObject.GetComponent<UniqueCreature>().stats.movement;
        }

        public void StartTurn()
        {
            action = true;
            movementLeft = maxMovement;
            movementLeft = gameObject.GetComponent<UniqueCreature>().stats.movement;
            Debug.Log("Start turn called");
        }
        

        void Update()
        {
            if (health <= 0)
            {
                Destroy(transform.gameObject);
            }
        }
    }
}
