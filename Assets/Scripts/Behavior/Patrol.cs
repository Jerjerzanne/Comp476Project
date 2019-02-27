using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] routine;
    private int roomNb = 0;
    private float timer = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  Instruction.CurrentInstruction(agent, routine, roomNb, timer);
      
    }
}
