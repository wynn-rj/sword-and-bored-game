using SwordAndBored.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding : IModelable, IDescriptable
{
    float BuildTime { get; set; }
    
}
