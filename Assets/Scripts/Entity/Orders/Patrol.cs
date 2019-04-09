using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : Order
{

    #region Variables

    [HideInInspector]
    public Entity agent;
    private int roomNb = 0;
    private float timer = 1;
    

    #endregion
        
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
        }
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
