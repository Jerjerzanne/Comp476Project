using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


[System.Serializable]
public class MyInstructionEvent : UnityEvent<Instruction>
{
}

/// <summary>
/// Manages all AI entity
/// </summary>
public class Entity : Destructible
{
    #region Variables

    //Editor fields
    [SerializeField, Header("Entity")]
    private Order initialOrder;

    public MyInstructionEvent instructionEvent;

    #endregion

    #region Properties

    /// <summary>
    /// Current order of the AI entity
    /// </summary>
    public Order CurrentOrder
    {
        get;
        set; // TODO: Remove instructions from stack when new order given
    }

    /// <summary>
    /// Current instruction of the AI entity
    /// </summary>
    public Instruction CurrentInstruction { get; set; }

    /// <summary>
    /// Instruction stack of the AI entity
    /// </summary>
    public Stack<Instruction> Instructions { get; set; }
    #endregion

    #region Methods

    /// <summary>
    /// Retrieve the next instruction from the order's routine
    /// </summary>
    private void GetNextInstruction()
    {
        if (Instructions.Count > 0)
        {
            CurrentInstruction = Instructions.Pop();
        }
        else
        {
            Debug.Log(this.name + " ran out of instructions.");
        }
    }

    /// <summary>
    /// Called when the current instruction is over (event linked)
    /// </summary>
    /// <param name="extentedInstruction"></param>
    private void EndOfInstruction(Instruction extentedInstruction)
    {
        if (extentedInstruction != null)
        {
            Instructions.Push(extentedInstruction);
        }
        else
        {
            GetNextInstruction();
        }
    }
    #endregion

    #region Functions

    void Start()
    {
       
        if (instructionEvent == null)
            instructionEvent = new MyInstructionEvent();

        instructionEvent.AddListener(EndOfInstruction);
    }

    private void Awake()
    {
        Instructions = new Stack<Instruction>();
        CurrentOrder = this.initialOrder;
        CurrentOrder.ExtractInstructions(this);

        // Insert all instructions to the entity's instruction stack
        for (int i = 0; i < CurrentOrder.instructions.Count; i++)
        {
            Debug.Log(CurrentOrder.instructions[i]);
            Debug.Log(Instructions);
            Instructions.Push(CurrentOrder.instructions[i]);
        }
        GetNextInstruction();
    }

    private void Update()
    {
        CurrentInstruction.Execute();
    }
    #endregion


}
