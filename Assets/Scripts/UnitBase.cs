using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : MonoBehaviour
{
    

    NavMeshAgent agent;

    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        
    }

    public void SetDestination(Vector3 pos)
    {
        agent.destination = pos;
    }
}
