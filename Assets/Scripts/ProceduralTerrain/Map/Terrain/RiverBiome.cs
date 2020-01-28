using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverBiome : AbstractBiome
{
    public RiverBiome() : base("RiverBiome")
    {
        this.heightModifier = 0.5f;
    }
}
