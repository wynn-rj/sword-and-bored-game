using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid;
using UnityEngine;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain
{
    class CreepComponent : AbstractCellComponent
    {
        
        public bool IsCreepActive
        {
            get => isCreepActive;
            set => ChangeCreepState(value);
        }

        private GameObject tileHolder;
        private bool isCreepActive;

        public CreepComponent(bool isCreepActive)
        {
            this.isCreepActive = isCreepActive;
        }

        public void SetTileHolder(GameObject tileHolder)
        {
            this.tileHolder = tileHolder;
            ChangeCreepState(isCreepActive);
        }

        private void ChangeCreepState(bool isActive)
        {
            isCreepActive = isActive;
            foreach (Transform child in tileHolder.transform)
            {
                if (child.name == Constants.creepObjectName)
                {
                    child.gameObject.SetActive(isActive);
                } 
                else if (child.name == Constants.terrainObjectName)
                {
                    child.gameObject.SetActive(!isActive);
                }
            }
        }
    }
}
