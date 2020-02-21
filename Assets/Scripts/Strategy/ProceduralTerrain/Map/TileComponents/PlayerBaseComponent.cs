using UnityEngine.SceneManagement;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents
{
    class PlayerBaseComponent : AbstractSelectionComponent
    {
        public override void Select()
        {
            SceneManager.LoadSceneAsync("BaseManagement", LoadSceneMode.Single);
        }
    }
}
