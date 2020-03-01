using UnityEngine;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid
{
    class GameObjectComponent : AbstractCellComponent
    {
        public GameObject GameObject { get; }

        public GameObjectComponent(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}
