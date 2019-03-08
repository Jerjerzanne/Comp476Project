using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchRoom : Instruction
{

    public SearchRoom( Entity entity) : base(entity)
    {

    }

    override public void Execute()
    {
        // Set of points to travel to
        // TODO: Poll room graph for points!

    }
}
