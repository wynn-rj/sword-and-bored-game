﻿using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    [CreateAssetMenu(fileName = "New Role Base Entry", menuName = "Role Entry")]
    public class RoleEntry : ScriptableObject
    {
        public string RoleName;
        public string RoleDescription;
        public Sprite RoleArtwork;
    }
}
