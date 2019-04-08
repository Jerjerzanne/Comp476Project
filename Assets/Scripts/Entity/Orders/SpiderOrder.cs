using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderOrder : Order
{
    public GameObject nest;
    public float timer = 1;
    public float visionrange = 3;
    public float nestrange = 8;

    override public void ExtractInstructions(Entity entity)
    {
        instructions = new List<Instruction>();
        
        instructions.Add(new Wander(nest.transform.position, timer,visionrange,nestrange, entity));
    }
}
