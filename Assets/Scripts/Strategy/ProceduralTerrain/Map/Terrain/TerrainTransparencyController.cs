using SwordAndBored.Utilities.Debug;
using UnityEngine;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain
{
    class TerrainTransparencyController : MonoBehaviour
    {
        [SerializeField] private GameObject terrain;
        [SerializeField] private float transparencyAmount = 0.8f;
        [SerializeField] private float transparencySpeed = 0.5f;
        private float currentTransparency;
        private Renderer[] terrainRenderers;
        private float goalTransparency;

        private void Awake()
        {
            AssertHelper.IsSetInEditor(terrain, this);
            terrainRenderers = terrain.GetComponentsInChildren<Renderer>();
            currentTransparency = terrainRenderers[0].material.GetColor("_BaseColor").a;
            goalTransparency = currentTransparency;
        }

        private void Update()
        {
            if (goalTransparency != currentTransparency)
            {
                currentTransparency = Mathf.Lerp(currentTransparency, goalTransparency, transparencySpeed);
                SetTransparency(currentTransparency);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            goalTransparency = transparencyAmount;
        }

        private void OnTriggerExit(Collider other)
        {
            goalTransparency = 0;
        }

        private void SetTransparency(float transparency)
        {
            foreach (Renderer renderer in terrainRenderers)
            {
                Color color = renderer.material.GetColor("_BaseColor");
                color.a = (1 - transparency);
                renderer.material.SetColor("_BaseColor", color);
            }
        }
    }
}
