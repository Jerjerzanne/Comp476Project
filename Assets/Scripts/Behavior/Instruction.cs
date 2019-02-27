using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Instruction : MonoBehaviour
{
    public enum Instructions
    {
        GOTO = 0, INTERACT = 1
    }

    public Instructions givenInstruction { set; get; }

    public NavMeshAgent agent;
    public Transform[] routine;
    private int roomNb = 0;
    private float timer = 1;
    private float callForOrder;
    void Start()
    {
    }

    void Update()
    {
        CurrentInstruction();
    }
    void CurrentInstruction()
    {
        switch (givenInstruction)
        {
            case Instructions.GOTO:
                Debug.Log("GoTo");
                if (timer > 0)
                {
                    if (transform.position.x != routine[roomNb].position.x && transform.position.z != routine[roomNb].transform.position.z)
                    {
                        agent.SetDestination(routine[roomNb].transform.position);
                    }
                    else
                    {
                        timer -= Time.deltaTime;
                    }
                }
                else
                {
                    timer = 1;
                    roomNb++;
                }
                if (roomNb > routine.Length - 1)
                {
                    roomNb = 0;
                }
                break;
            case Instructions.INTERACT:
                if (transform.position.x != routine[roomNb].position.x && transform.position.z != routine[roomNb].transform.position.z)
                {
                    agent.SetDestination(routine[roomNb].transform.position);
                }
                else
                {
                    Instruction interactableInteraction = this.GetComponent<Interactable>().EntityInteract;
                }
                break;
        }
        
    }
}
