using UnityEngine;

namespace SwordAndBored.Strategy.Transitions
{
    static class SceneSharing
    {
        public static ulong timeStep;
        public static bool useStoredTimeStep = false;
        public static Vector3 cameraPosition;
        public static int gold;
    }
}
