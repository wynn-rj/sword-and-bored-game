using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    [CreateAssetMenu(fileName = "New Role Base Entry", menuName = "Role Entry")]
    public class RoleEntry : ScriptableObject
    {
        public string roleName;
        public string roleDescription;
        public int roleCost;
        public Sprite roleArtwork;
    }
}
