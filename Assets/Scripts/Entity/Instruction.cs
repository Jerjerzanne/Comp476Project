using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Instruction
{
    //public enum InstructionType
    //{
    //    GOTO = 0, INTERACT = 1
    //}

    // The entity that runs this instruction
    protected Entity instructionRunner;

    //public InstructionType givenInstruction { set; get; }
    //private Instruction returnedInstruction;
    //public NavMeshAgent agent;
    //public Transform[] routine;

    #region Methods

    protected Instruction(Entity entity)
    {
        instructionRunner = entity;
    }

    /// <summary>
    /// Base function for all instructions. Called when we run an instruction.
    /// </summary>
    /// <param name="entityAgent"></param>
    virtual public void Execute()
    {
        
    }

#endregion

#region Functions

#endregion

}
