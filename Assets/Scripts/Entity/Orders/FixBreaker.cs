using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixBreaker : Order
{
    public float searchTimer;
    public float navigationTimer;


    override public void ExtractInstructions(Entity entity)
    {
        instructions = new List<Instruction>();
        foreach (GameObject obj in routine)
        {
            // For each breaker to repair, add a goto, an interact, and a searchroom:
            Breaker breakerObj = obj.GetComponent<Breaker>();
            // Get the position of the collider:
            Vector3 colliderPos = breakerObj.GetComponent<Collider>().transform.position;

            instructions.Add(new Goto(colliderPos, 0, entity));
            instructions.Add(new Interact(breakerObj, entity));
            instructions.Add(new SearchRoom(breakerObj.GetComponentInParent<Room>(), searchTimer, navigationTimer, entity));
        }
    }
}
