using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goto : Instruction
{
    float acceptanceRadius = 0.1f;
    public Vector3 location;
    //private float callForOrder;
    //private int roomNb = 0;
    private float timer = 1;
    NavMeshAgent entityAgent;

    #region Methods

    public Goto(Vector3 location, float timer, Entity entity): base(entity)
    {
        this.location = location;
        this.timer = timer;
        entityAgent = instructionRunner.GetComponent<NavMeshAgent>();
    }

    override public void Execute()
    {
        if (Mathf.Abs(instructionRunner.transform.position.x - location.x) > acceptanceRadius ||
                Mathf.Abs(instructionRunner.transform.position.z - location.z) > acceptanceRadius)
        {
            entityAgent.SetDestination(location);
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                instructionRunner.instructionEvent.Invoke(null);
            }
        }
    }
    #endregion
}
