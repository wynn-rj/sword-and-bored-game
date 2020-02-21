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
                currentTile.UnitOnTile = null;
                currentTile = null;
            }
            agent.destination = pos;
        }

        public void SetTile(Tile tile)
        {
            currentTile = tile;
            tile.UnitOnTile = this.gameObject;
        }
    }
}
