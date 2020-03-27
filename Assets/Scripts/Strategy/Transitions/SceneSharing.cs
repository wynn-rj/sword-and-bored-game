using UnityEngine;

namespace SwordAndBored.Strategy.Transitions
{
    static class SceneSharing
    {
        public static Vector3 cameraPosition;
        public static int squadID = -1;
        public static float enemyMood = 0;
        public static float enemyProductivity = 0;
        public static float enemyProductivityDirection = 0;
        public static int enemyControlledLocations = 1;
    }
}
