﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


[System.Serializable]
public class MyInstructionEvent : UnityEvent<Instruction>
{
}

[System.Serializable]
public class MyReactionEvent : UnityEvent<GameObject[]>
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

    private Order _currentOrder;

    public MyInstructionEvent instructionEvent;

    public MyReactionEvent reactionEvent;

    #endregion
    
    #region Properties

    /// <summary>
    /// Current order of the AI entity
    /// </summary>
    public Order CurrentOrder
    {
        get { return _currentOrder; }
        set
        {
            _currentOrder = value;
            CurrentOrder.ExtractInstructions(this);

            Instructions.Clear();
            CurrentOrder.instructions.Reverse();
            // Insert all instructions to the entity's instruction stack
            for (int i = 0; i < CurrentOrder.instructions.Count; i++)
            {
                Debug.Log(CurrentOrder.instructions[i]);
                Debug.Log(Instructions);
                Instructions.Push(CurrentOrder.instructions[i]);
            }
            GetNextInstruction();
        } 
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
            CurrentInstruction = null;
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
            CurrentInstruction = Instructions.Pop();
        }
        else
        {
            GetNextInstruction();
        }
    }

    /// <summary>
    /// How the entity reacts to a detection event
    /// </summary>
    /// <param name="target"></param>
    virtual protected void DetectionReaction(GameObject[] target)
    {
        
    }
    #endregion

    #region Functions

    void Start()
    {
       
       
    }

    private void Awake()
    {

        if (instructionEvent == null)
            instructionEvent = new MyInstructionEvent();

        if (reactionEvent == null)
            reactionEvent = new MyReactionEvent();

        instructionEvent.AddListener(EndOfInstruction);
        reactionEvent.AddListener(DetectionReaction);

        Instructions = new Stack<Instruction>();
        CurrentOrder = this.initialOrder;
    }

    protected void Update()
    {
        if (CurrentInstruction != null)
        {
            CurrentInstruction.Execute();
        }
    }
    #endregion


}
