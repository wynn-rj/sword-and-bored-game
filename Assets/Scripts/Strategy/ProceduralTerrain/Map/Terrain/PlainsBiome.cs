using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainsBiome : AbstractBiome
{
    public PlainsBiome() : base("PlainsBiome")
    {
        this.heightModifier = 1f;
    }
}
