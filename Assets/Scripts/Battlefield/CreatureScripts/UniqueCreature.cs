using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueCreature : CreatureBase
{
    [Header("Material Info")]
    public Material[] mat;
    [HideInInspector]
    public Renderer currentMat;
    float a = .05f;
    float b;
    int highlightColor;
    [HideInInspector]
    public UnitAbilitiesContainer abil;
    [HideInInspector]
    public UnitStats stats;

    void Start()
    {
        health = maxHealth;

        currentMat = GetComponent<Renderer>();
        abil = GetComponent<UnitAbilitiesContainer>();
        stats = GetComponent<UnitStats>();
    }

    

    public void Glow(int glow)
    {
        highlightColor = glow;
        if (glow == 1)
        {
            currentMat.material = mat[1];
        } else if (glow == 2)
        {
            currentMat.material = mat[0];
        } else if (glow == 3) 
        {
            b = a + Time.time;
            currentMat.material = mat[2];
        }
    }

    private void Update()
    {
        if (highlightColor == 3 && Time.time > b)
        {
            Glow(2);
        }
    }
}
