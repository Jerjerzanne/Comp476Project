using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Instruction : MonoBehaviour
{
    //public enum InstructionType
    //{
    //    GOTO = 0, INTERACT = 1
    //}

    //public InstructionType givenInstruction { set; get; }
    //private Instruction returnedInstruction;
    //public NavMeshAgent agent;
    //public Transform[] routine;
    public Transform location;
    //private int roomNb = 0;
    private float timer = 1;
    //private float callForOrder;

    #region Methods

    public void Execute(Entity entity, NavMeshAgent entityAgent)
    {
        //switch (givenInstruction)
        //{
        //    case InstructionType.GOTO:
        //Debug.Log("GoTo");


        if (entity.transform.position.x != location.position.x && entity.transform.position.z != location.transform.position.z)
        {
            entityAgent.SetDestination(location.transform.position);
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                entity.instructionEvent.Invoke(null);
            }
        }

        //break;
        //case InstructionType.INTERACT:
        //    if (entity.transform.position.x != routine[roomNb].position.x && entity.transform.position.z != routine[roomNb].transform.position.z)
        //    {
        //        entityAgent.SetDestination(routine[roomNb].transform.position);
        //    }
        //    else
        //    {
        //        Instruction interactableInteraction = this.GetComponent<Interactable>().EntityInteract(entity);
        //        entity.instructionEvent.Invoke(interactableInteraction);
        //    }
        //    break;
    }

#endregion

#region Functions

#endregion

}
