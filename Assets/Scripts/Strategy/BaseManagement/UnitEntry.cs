using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Base Entry", menuName = "Unit Entry")]
public class UnitEntry : ScriptableObject
{
    public string RoleName;
    public string RoleDescription;
    public Sprite RoleArtwork;
}
