using UnityEngine;

namespace SwordAndBored.TurnMechanism
{
    public class TurnOrderController
    {
        private readonly GameObject[] entities;
        private int pointer = 0;

        public TurnOrderController(GameObject[] entities)
        {
            this.entities = entities;
        }

        public TurnOrderController(GameObject[] entities, IShuffler<GameObject> shuffler)
        : this(shuffler.shuffle(entities)) { }

        public GameObject NextEntity()
        {
            GameObject nextEntity = entities[pointer];
            pointer += 1;
            pointer %= entities.Length;

            return nextEntity;
        }

    }
}