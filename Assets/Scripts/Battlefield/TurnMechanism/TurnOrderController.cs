using UnityEngine;
using System.Collections.Generic;
using SwordAndBored.Battlefield.CreaturScripts;

namespace SwordAndBored.Battlefield.TurnMechanism
{
    public class TurnOrderController
    {
        private readonly List<GameObject> entities;
        private int pointer = 0;

        public TurnOrderController(GameObject[] entities)
        {
            //this.entities = entities;
            List<GameObject> list = new List<GameObject>();
            list.AddRange(entities);
            list.Sort(delegate (GameObject a, GameObject b)
            {
                return b.GetComponent<UniqueCreature>().stats.speedIntit.CompareTo(a.GetComponent<UniqueCreature>().stats.speedIntit);
            }
            );
            this.entities = list;
        }

        public TurnOrderController(GameObject[] entities, IShuffler<GameObject> shuffler)
        : this(shuffler.shuffle(entities)) { }
        /*{
            List<GameObject> list = new List<GameObject>();
            list.AddRange(entities);
            list.Sort(delegate (GameObject a, GameObject b)
            {
                return b.GetComponent<UniqueCreature>().stats.speedIntit.CompareTo(a.GetComponent<UniqueCreature>().stats.speedIntit);
            }
            );
            this.entities = list.ToArray();
        }*/

        public GameObject NextEntity()
        {
            if (pointer >= entities.Count)
            {
                pointer -= 1;
            }
            GameObject nextEntity = entities[pointer];
            pointer += 1;
            pointer %= entities.Count;

            return nextEntity;
        }

        public void RemoveEntity(GameObject unit)
        {
            entities.Remove(unit);
        }

    }
}