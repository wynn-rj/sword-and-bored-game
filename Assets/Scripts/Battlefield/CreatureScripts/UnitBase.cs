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

    
        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }
    

        protected void MoveTo(Vector3 pos)
        {
            if (currentTile)
            {
                currentTile.unitOnTile = null;
                currentTile = null;
            }
            agent.destination = pos;
        }

        static public int Roll(int dice)
        {
            return Random.Range(1, dice);
        }

        static public int Roll()
        {
            return Roll(20);
        }

        public void SetTile(Tile tile)
        {
            currentTile = tile;
            tile.unitOnTile = this.gameObject;
        }
    }
}
