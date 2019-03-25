using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : Order
{
    private float timer = 10.0f;

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
            instructions.Add(new Spawn(obj.transform.position + obj.transform.forward, timer, entity));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
