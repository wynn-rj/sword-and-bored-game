using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : MonoBehaviour
{
    

    NavMeshAgent agent;

    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    

    public void MoveTo(Vector3 pos)
    {
        agent.destination = pos;
    }

    static public int Roll(int dice)
    {
        return Random.Range(1, dice);
    }

    static public int Roll()
    {
        return Roll(20);
    }
}
