using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nest : Order
{
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
            // Spawn
            //instructions.Add(new Spawn(obj.transform.position, timer, entity));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
