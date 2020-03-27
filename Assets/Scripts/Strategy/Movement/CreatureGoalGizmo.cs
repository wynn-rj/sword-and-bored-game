using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using System.Reflection;
using UnityEngine;

namespace SwordAndBored.Strategy.Movement
{
    class CreatureGoalGizmo : MonoBehaviour
    {
        [SerializeField] private CreatureMovementController creature;
        [SerializeField] private Vector3 boxSize = new Vector3(2, 20, 2);
        private object creaturePath;


        private void Start()
        {
            creaturePath = creature.GetType().GetField("path", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(creature);
        }

        private void OnDrawGizmosSelected()
        {
            if (creaturePath == null)
            {
                 creaturePath = creature.GetType().GetField("path", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(creature);
            }
            IHexGridCell destination = (IHexGridCell)(creaturePath.GetType().GetProperty("Last", BindingFlags.Public | BindingFlags.Instance).GetValue(creaturePath));
            if (destination == null)
            {
                destination = creature.Location;
            }
            Gizmos.color = Color.red;
            Gizmos.DrawCube(destination.Position.CenterAsVector3(boxSize.y / 2), boxSize);
        }
    }
}
