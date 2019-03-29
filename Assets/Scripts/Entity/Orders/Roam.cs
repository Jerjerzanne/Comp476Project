using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roam : Order
{
    NavMeshAgent agent;
    private int roomNb = 0;
    private float timer = 1;
    void Start()
    {

    }

    private void Awake()
    {
    }

    override public void ExtractInstructions(Entity entity)
    {
        instructions = new List<Instruction>();
        foreach (GameObject obj in routine)
        {
            instructions.Add(new Goto(obj.transform.position, timer, entity));
            instructions.Add(new CreateNest(entity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  Instruction.CurrentInstruction(agent, routine, roomNb, timer);

    }
}
