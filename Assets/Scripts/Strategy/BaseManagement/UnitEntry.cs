using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement.SO
{
    [CreateAssetMenu(fileName = "New Unit Base Entry", menuName = "Unit Entry")]
    public class UnitEntry : ScriptableObject
    {
        public string RoleName;
        public string RoleDescription;
        public Sprite RoleArtwork;
    }
}
