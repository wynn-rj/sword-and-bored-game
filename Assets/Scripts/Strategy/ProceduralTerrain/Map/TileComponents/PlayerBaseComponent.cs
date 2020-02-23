using UnityEngine.SceneManagement;
using SwordAndBored.SceneManagement;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents
{
    class PlayerBaseComponent : AbstractSelectionComponent
    {
        public override void Select()
        {
            SceneManager.LoadSceneAsync(GameScenes.BASEVIEW);
        }
    }
}
