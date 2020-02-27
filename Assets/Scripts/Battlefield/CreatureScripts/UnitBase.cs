using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace SwordAndBored.Battlefield.CreaturScripts
{

    public class UnitBase : MonoBehaviour
    {
    
        [HideInInspector]
        public NavMeshAgent agent;
        [HideInInspector]
        public Tile currentTile;

        [Header("Creature Info")]
        public string creatureName;
        public int maxHealth;
        public int health;
        public Animator anim;

        [HideInInspector]
        public bool action = true;

        public int maxMovement;
        int movement;


        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

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


        protected void MoveTo(Vector3 pos)
        {
            agent.destination = pos;
        }

        public void SetTile(Tile tile)
        {
            currentTile.unitOnTile = null;
            currentTile = tile;
            tile.unitOnTile = this.gameObject;
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
