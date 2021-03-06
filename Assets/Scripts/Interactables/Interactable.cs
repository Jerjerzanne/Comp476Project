﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable game entity
/// </summary>
public class Interactable : Destructible
{
    #region Variables


    public Instruction nextInstruction;

    protected float cooldownTime = 0.3f;
    // When checking for cooldown, we want to do it as such:
    // currentTime - lastInteractionTime > cooldownTime
    protected static float lastInteractionTime;
    //editor variables

    #endregion

    #region Methods

    /// <summary>
    /// Manages interactions between the interactable and the entity
    /// </summary>
    public virtual Instruction EntityInteract(Entity entity)
    {
        return nextInstruction;
    }

    /// <summary>
    /// Manages interactions between the interactable and the player
    /// </summary>
    public virtual void PlayerInteract(Player player)
    {

    }

    protected override void Die() 
    {
        Debug.Log(this.name + " is now broken.");
    }

    /// <summary>
    /// Function that is called by the Repair instruction when an object is done being repaired.
    /// </summary>
    public virtual void Repair()
    {

    }

    #endregion

}
