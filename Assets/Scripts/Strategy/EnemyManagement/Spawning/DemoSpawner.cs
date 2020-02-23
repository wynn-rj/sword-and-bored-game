using SwordAndBored.Utilities.Debug;
using UnityEngine;

namespace SwordAndBored.Strategy.EnemyManagement.Spawning
{
    public class DemoSpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;

#if DEBUG
        void Awake()
        {
            AssertHelper.IsSetInEditor(enemyPrefab, this);
        }
#endif

        void Start()
        {            
            // Place an enemy in each biome
            PlaceEnemy(-147, 16, -157.6166f);
            PlaceEnemy(-111, 1, -116.0474f);
            PlaceEnemy(-87, 5, -122.9756f);
            PlaceEnemy(-45, 4, 1.73205f);
            PlaceEnemy(-39, 21, 677.54998f);
            PlaceEnemy(96, 16, 96.99484f);
        }

        private void PlaceEnemy(float x, float y, float z)
        {
            // Place on top of hex tile
            y += 7;
            Instantiate(enemyPrefab, new Vector3(x, y, z), Quaternion.identity).transform.parent = transform;
        }
    }
}
