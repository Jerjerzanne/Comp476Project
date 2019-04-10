using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : Instruction
{
    private Vector3 targetLocation;
    NavMeshAgent entityAgent;

    #region Methods

    public Chase(Vector3 targetLocation, Entity entity): base(entity)
    {
        this.targetLocation = targetLocation;
        entityAgent = instructionRunner.GetComponent<NavMeshAgent>();
        
    }

    public override void Execute()
    {
        //Debug.Log("Beginning chase instruction.");

        if (instructionRunner.transform.position.x != targetLocation.x && instructionRunner.transform.position.z != targetLocation.z)
        {
            entityAgent.SetDestination(targetLocation);
        }
        else
        {
            Debug.Log("target has been lost. Returning to previous behavior.");
            instructionRunner.instructionEvent.Invoke(null); //TODO: add search room
        }
    }
    #endregion


}
